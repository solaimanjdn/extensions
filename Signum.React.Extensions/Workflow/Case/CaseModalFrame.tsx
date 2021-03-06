﻿
import * as React from 'react'
import { Dic } from '../../../../Framework/Signum.React/Scripts/Globals'
import { Modal, ModalProps, ModalClass, ButtonToolbar, Button } from 'react-bootstrap'
import { openModal, IModalProps } from '../../../../Framework/Signum.React/Scripts/Modals'
import { TypeContext, StyleOptions, EntityFrame  } from '../../../../Framework/Signum.React/Scripts/TypeContext'
import { TypeInfo, getTypeInfo, parseId, GraphExplorer, PropertyRoute, ReadonlyBinding, } from '../../../../Framework/Signum.React/Scripts/Reflection'
import * as Navigator from '../../../../Framework/Signum.React/Scripts/Navigator'
import ModalMessage from '../../../../Framework/Signum.React/Scripts/Modals/ModalMessage'
import * as Operations from '../../../../Framework/Signum.React/Scripts/Operations'
import { EntityPack, Entity, Lite, JavascriptMessage, NormalWindowMessage, entityInfo, getToString, toLite } from '../../../../Framework/Signum.React/Scripts/Signum.Entities'
import { renderWidgets, renderEmbeddedWidgets, WidgetContext } from '../../../../Framework/Signum.React/Scripts/Frames/Widgets'
import ValidationErrors from '../../../../Framework/Signum.React/Scripts/Frames/ValidationErrors'
import ButtonBar from '../../../../Framework/Signum.React/Scripts/Frames/ButtonBar'
import { CaseActivityEntity, WorkflowEntity, ICaseMainEntity, CaseActivityOperation } from '../Signum.Entities.Workflow'
import * as WorkflowClient from '../WorkflowClient'
import CaseFromSenderInfo from './CaseFromSenderInfo'
import CaseButtonBar from './CaseButtonBar'
import CaseFlowButton from './CaseFlowButton'
import InlineCaseTags from './InlineCaseTags'

require("../../../../Framework/Signum.React/Scripts/Frames/Frames.css");
require("./Case.css");

interface CaseModalFrameProps extends React.Props<CaseModalFrame>, IModalProps {
    title?: string;
    entityOrPack: Lite<CaseActivityEntity> | CaseActivityEntity | WorkflowClient.CaseEntityPack;
    avoidPromptLooseChange?: boolean;
    readOnly?: boolean;
    isNavigate?: boolean;
}

interface CaseModalFrameState {
    pack?: WorkflowClient.CaseEntityPack;
    getComponent?: (ctx: TypeContext<ICaseMainEntity>) => React.ReactElement<any>;
    show: boolean;
    prefix?: string;
}

var modalCount = 0;

export default class CaseModalFrame extends React.Component<CaseModalFrameProps, CaseModalFrameState>  {
    prefix = "caseModal" + (modalCount++)
    constructor(props: any) {
        super(props);
        this.state = this.calculateState(props);
    }

    componentWillMount() {
        WorkflowClient.toEntityPackWorkflow(this.props.entityOrPack)
            .then(ep => this.setPack(ep))
            .then(() => this.loadComponent())
            .done();
    }

    componentWillReceiveProps(props: any) {
        this.setState(this.calculateState(props));

        WorkflowClient.toEntityPackWorkflow(this.props.entityOrPack)  
            .then(ep => this.setPack(ep))
            .then(() => this.loadComponent())
            .done();
    }

    calculateState(props: CaseModalFrameState): CaseModalFrameState {
        return {
            show: true,
        };
    }

    setPack(pack: WorkflowClient.CaseEntityPack): void {
        this.setState({ pack: pack });
    }

    loadComponent(): Promise<void> {
        const a = this.state.pack!.activity;
        if (a.workflowActivity) {
            return WorkflowClient.getViewPromise(a.case.mainEntity, a.workflowActivity.viewName).promise
                .then(c => this.setState({ getComponent: c }));
        }
        else {
            return Navigator.getViewPromise(a.case.mainEntity).promise
                .then(c => this.setState({ getComponent: c }));
        }
    }

    handleCloseClicked = () => {

        if (this.hasChanges() && !this.props.avoidPromptLooseChange) {
            ModalMessage.show({
                title: NormalWindowMessage.ThereAreChanges.niceToString(),
                message: NormalWindowMessage.LoseChanges.niceToString(),
                buttons: "yes_no",
                icon: "warning",
                defaultStyle: "warning"
            }).then(result => {
                if (result != "yes")
                return;

                this.setState({ show: false });
            }).done();
        }
        else
        this.setState({ show: false });
    }

    hasChanges() {

        var entity = this.state.pack!.activity;

        GraphExplorer.propagateAll(entity);

        return entity.modified;
    }

    okClicked: boolean;
    handleCancelClicked = () => {

        if (this.hasChanges() && !this.props.avoidPromptLooseChange) {
            if (!confirm(NormalWindowMessage.LoseChanges.niceToString()))
                return;
        }

        this.setState({ show: false });
    }
    
    handleOkClicked = (val: any) => {
        if (this.hasChanges()) {
            alert(JavascriptMessage.saveChangesBeforeOrPressCancel.niceToString());
            return;
        }

        this.okClicked = true;
        this.setState({ show: false });
    }

    handleOnExited = () => {
        this.props.onExited!(this.okClicked ? this.getCaseActivity() : undefined);
    }

    getCaseActivity() {
        return this.state.pack && this.state.pack.activity;
    }

    render() {

        var pack = this.state.pack;

        return (
            <Modal bsSize="lg" onHide={this.handleCloseClicked} show={this.state.show} onExited={this.handleOnExited} className="sf-popup-control">
                <Modal.Header closeButton={this.props.isNavigate}>
                    {!this.props.isNavigate && <ButtonToolbar className="pull-right flip">
                        <Button className="sf-entity-button sf-close-button sf-ok-button" bsStyle="primary" disabled={!pack} onClick={this.handleOkClicked}>{JavascriptMessage.ok.niceToString()}</Button>
                        <Button className="sf-entity-button sf-close-button sf-cancel-button" bsStyle="default" disabled={!pack} onClick={this.handleCancelClicked}>{JavascriptMessage.cancel.niceToString()}</Button>
                    </ButtonToolbar>}
                    {this.renderTitle() }
                </Modal.Header>
                {pack && this.renderBody() }
            </Modal>
        );
    }

    entityComponent: React.Component<any, any>;

    setComponent(c: React.Component<any, any>) {
        if (c && this.entityComponent != c) {
            this.entityComponent = c;
            this.forceUpdate();
        }
    }

    renderBody() {
        var pack = this.state.pack!;

        var activityFrame: EntityFrame<CaseActivityEntity> = {
            frameComponent: this,
            entityComponent: this.entityComponent,
            onReload: newPack => {
                pack.activity = newPack.entity;
                pack.canExecuteActivity = newPack.canExecute;
                this.forceUpdate();
            },
            onClose: (ok?: boolean) => this.props.onExited!(ok ? this.getCaseActivity() : undefined),
            revalidate: () => this.validationErrors && this.validationErrors.forceUpdate(),
            setError: (modelState, initialPrefix) => {
                GraphExplorer.setModelState(pack.activity, modelState, initialPrefix || "");
                this.forceUpdate();
            },
        };

        var activityPack = { entity: pack.activity, canExecute: pack.canExecuteActivity };
        
        return (
            <Modal.Body>
                <CaseFromSenderInfo current={pack.activity} />
                {!pack.activity.case.isNew && <div className="inline-tags"> <InlineCaseTags case={toLite(pack.activity.case)} /></div>}
                <div className="sf-main-control form-horizontal" data-test-ticks={new Date().valueOf() } data-activity-entity={entityInfo(pack.activity) }>
                    { this.renderMainEntity() }
                </div>
                {this.entityComponent && <CaseButtonBar frame={activityFrame} pack={activityPack} />}
            </Modal.Body>
        );
    }

    validationErrors: ValidationErrors;

    getMainTypeInfo(): TypeInfo {
        return getTypeInfo(this.state.pack!.activity.case.mainEntity.Type);
    }

    renderMainEntity() {

        var pack = this.state.pack!;
        var mainEntity = pack.activity.case.mainEntity;
        const mainFrame: EntityFrame<ICaseMainEntity> = {
            frameComponent: this,
            entityComponent: this.entityComponent,
            onReload: newPack => {
                pack.activity.case.mainEntity = newPack.entity;
                pack.canExecuteMainEntity = newPack.canExecute;
                this.forceUpdate();
            },
            onClose: () => this.props.onExited!(null),
            revalidate: () => this.validationErrors && this.validationErrors.forceUpdate(),
            setError: (ms, initialPrefix) => {
                GraphExplorer.setModelState(mainEntity, ms, initialPrefix || "");
                this.forceUpdate()
            },
        };

        var ti = this.getMainTypeInfo();

        const styleOptions: StyleOptions = {
            readOnly: Navigator.isReadOnly(ti) || Boolean(pack.activity.doneDate),
            frame: mainFrame
        };

        const ctx = new TypeContext<ICaseMainEntity>(undefined, styleOptions, PropertyRoute.root(ti), new ReadonlyBinding(mainEntity, this.prefix));

        var mainPack = { entity: mainEntity, canExecute: pack.canExecuteMainEntity };

        const wc: WidgetContext = {
            ctx: ctx,
            pack: mainPack,
        };
        
        return (
            <div className="sf-main-entity case-main-entity" data-main-entity={entityInfo(mainEntity)}>
                {renderWidgets(wc)}
                {this.entityComponent && !mainEntity.isNew && !pack.activity.doneBy && <ButtonBar frame={mainFrame} pack={mainPack} />}
                <ValidationErrors entity={mainEntity} ref={ve => this.validationErrors = ve} />
                {this.state.getComponent && React.cloneElement(this.state.getComponent(ctx), { ref: (c: React.Component<any, any>) => this.setComponent(c) })}
            </div>
        );
    }

    renderTitle() {

        if (!this.state.pack)
            return <h3>{JavascriptMessage.loading.niceToString() }</h3>;

        const activity = this.state.pack.activity;

        return (
            <h4>
                <span className="sf-entity-title">{this.props.title || getToString(activity) }</span>&nbsp;
                {this.renderExpandLink() }
                <br />
                <CaseFlowButton caseActivity={this.state.pack.activity} />
                <small> {Navigator.getTypeTitle(activity, undefined)}</small>
            </h4>
        );
    }

    renderExpandLink() {
        const entity = this.state.pack!.activity;

        if (entity == null || entity.isNew)
            return null;

        const ti = getTypeInfo(entity.Type);

        if (ti == null || !Navigator.isNavigable(ti, false)) //Embedded
            return null;

        return (
            <a href={ "~/workflow/activity/" + entity.id } className="sf-popup-fullscreen" onClick={this.handlePopupFullScreen}>
                <span className="glyphicon glyphicon-new-window"></span>
            </a>
        );
    }

    handlePopupFullScreen = (e: React.MouseEvent<any>) => {

        if (e.ctrlKey || e.buttons) {

        } else {

            Navigator.currentHistory.push("~/workflow/activity/" + this.state.pack!.activity.id);

            e.preventDefault();
        }
    }

    static openView(entityOrPack: Lite<CaseActivityEntity> | CaseActivityEntity | WorkflowClient.CaseEntityPack, readOnly?: boolean): Promise<CaseActivityEntity> {

        return openModal<CaseActivityEntity>(<CaseModalFrame
            entityOrPack={entityOrPack}
            readOnly={readOnly || false}
            isNavigate={false}
        />);
    }


    static openNavigate(entityOrPack: Lite<CaseActivityEntity> | CaseActivityEntity | WorkflowClient.CaseEntityPack, readOnly? :boolean): Promise<void> {

        return openModal<void>(<CaseModalFrame
            entityOrPack={entityOrPack}
            readOnly={readOnly || false}
            isNavigate={true}
        />);
    }
}
