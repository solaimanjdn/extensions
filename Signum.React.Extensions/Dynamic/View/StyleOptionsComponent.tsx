﻿import * as React from 'react'
import { Modal, ModalProps, ModalClass, ButtonToolbar, Button } from 'react-bootstrap'
import { FormGroup, FormControlStatic, ValueLine, ValueLineType, EntityLine, EntityCombo, EntityList, EntityRepeater } from '../../../../Framework/Signum.React/Scripts/Lines'
import { classes, Dic } from '../../../../Framework/Signum.React/Scripts/Globals'
import * as Finder from '../../../../Framework/Signum.React/Scripts/Finder'
import { QueryDescription, SubTokensOptions, QueryToken, filterOperations, OrderType, ColumnOptionsMode } from '../../../../Framework/Signum.React/Scripts/FindOptions'
import { getQueryNiceName, getTypeInfo, isTypeEntity, Binding } from '../../../../Framework/Signum.React/Scripts/Reflection'
import * as Navigator from '../../../../Framework/Signum.React/Scripts/Navigator'
import { TypeContext, FormGroupStyle } from '../../../../Framework/Signum.React/Scripts/TypeContext'
import QueryTokenBuilder from '../../../../Framework/Signum.React/Scripts/SearchControl/QueryTokenBuilder'
import { ModifiableEntity, JavascriptMessage, EntityControlMessage } from '../../../../Framework/Signum.React/Scripts/Signum.Entities'
import { QueryEntity } from '../../../../Framework/Signum.React/Scripts/Signum.Entities.Basics'
import { FilterOperation, PaginationMode } from '../../../../Framework/Signum.React/Scripts/Signum.Entities.DynamicQuery'
import { ExpressionOrValueComponent, FieldComponent, DesignerModal } from './Designer'
import * as Nodes from './Nodes'
import * as NodeUtils from './NodeUtils'
import { DesignerNode, Expression, ExpressionOrValue } from './NodeUtils'
import { FindOptionsComponent } from './FindOptionsComponent'
import { BaseNode } from './Nodes'
import { StyleOptionsExpression, formGroupStyle, formGroupSize } from './StyleOptionsExpression'
import { openModal, IModalProps } from '../../../../Framework/Signum.React/Scripts/Modals';
import SelectorModal from '../../../../Framework/Signum.React/Scripts/SelectorModal';
import { DynamicViewMessage, DynamicViewValidationMessage } from '../Signum.Entities.Dynamic'
import * as DynamicViewClient from '../DynamicViewClient'
import Typeahead from '../../../../Framework/Signum.React/Scripts/Lines/Typeahead'

interface StyleOptionsLineProps {
    binding: Binding<StyleOptionsExpression | undefined>;
    dn: DesignerNode<BaseNode>;
}

export class StyleOptionsLine extends React.Component<StyleOptionsLineProps, void>{

    renderMember(expr: StyleOptionsExpression | undefined): React.ReactNode {
        return (<span
            className={expr === undefined ? "design-default" : "design-changed"}>
            {this.props.binding.member}
        </span>);
    }

    handleRemove = () => {
        this.props.binding.deleteValue();
        this.props.dn.context.refreshView();
    }

    handleCreate = () => {
        this.modifyExpression({} as StyleOptionsExpression);
    }

    handleView = (e: React.MouseEvent<any>) => {
        e.preventDefault();
        var hae = JSON.parse(JSON.stringify(this.props.binding.getValue())) as StyleOptionsExpression;
        this.modifyExpression(hae);
    }

    modifyExpression(soe: StyleOptionsExpression) {
        
        DesignerModal.show("StyleOptions", () => <StyleOptionsComponent dn={this.props.dn} styleOptions={soe} />).then(result => {
            if (result) {
              
                if (Dic.getKeys(soe).length == 0)
                    this.props.binding.deleteValue();
                else
                    this.props.binding.setValue(soe);
            }

            this.props.dn.context.refreshView();
        }).done();
    }

    render() {
        const val = this.props.binding.getValue();

        return (
            <div className="form-group">
                <label className="control-label">
                    {this.renderMember(val)}

                    {val && " "}
                    {val && <a className={classes("sf-line-button", "sf-remove")}
                        onClick={this.handleRemove}
                        title={EntityControlMessage.Remove.niceToString()}>
                        <span className="glyphicon glyphicon-remove" />
                    </a>}
                </label>
                <div>
                    {val ?
                        <a href="" onClick={this.handleView}><pre style={{ padding: "0px", border: "none" }}>{this.getDescription(val)}</pre></a>
                        :
                        <a title={EntityControlMessage.Create.niceToString()}
                            className="sf-line-button sf-create"
                            onClick={this.handleCreate}>
                            <span className="glyphicon glyphicon-plus sf-create sf-create-label" />{EntityControlMessage.Create.niceToString()}
                        </a>}
                </div>
            </div>
        );
    }

    getDescription(soe: StyleOptionsExpression) {

        var keys = Dic.map(soe as any, (key, value) => key + ":" + value);
        return keys.join("\n");
    }
}

export interface StyleOptionsComponentProps {
    dn: DesignerNode<BaseNode>;
    styleOptions: StyleOptionsExpression
}

export class StyleOptionsComponent extends React.Component<StyleOptionsComponentProps, void>{
    render() {
        const so = this.props.styleOptions;
        const dn = this.props.dn;

        return (
            <div className="form-sm code-container">
                <ExpressionOrValueComponent dn={dn} refreshView={() => this.forceUpdate()} binding={Binding.create(so, s => s.formGroupStyle)} type="string" options={formGroupStyle} defaultValue={null} />
                <ExpressionOrValueComponent dn={dn} refreshView={() => this.forceUpdate()} binding={Binding.create(so, s => s.formGroupSize)} type="string" options={formGroupSize} defaultValue={null} />
                <ExpressionOrValueComponent dn={dn} refreshView={() => this.forceUpdate()} binding={Binding.create(so, s => s.placeholderLabels)} type="boolean" defaultValue={null} />
                <ExpressionOrValueComponent dn={dn} refreshView={() => this.forceUpdate()} binding={Binding.create(so, s => s.formControlClassReadonly)} type="string" defaultValue={null} />
                <ExpressionOrValueComponent dn={dn} refreshView={() => this.forceUpdate()} binding={Binding.create(so, s => s.labelColumns)} type="number" defaultValue={null} />
                <ExpressionOrValueComponent dn={dn} refreshView={() => this.forceUpdate()} binding={Binding.create(so, s => s.valueColumns)} type="number" defaultValue={null} />
                <ExpressionOrValueComponent dn={dn} refreshView={() => this.forceUpdate()} binding={Binding.create(so, s => s.readOnly)} type="boolean" defaultValue={null} />
            </div>
        );
    }
}

