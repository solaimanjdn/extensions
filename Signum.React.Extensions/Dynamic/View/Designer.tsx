﻿import * as React from 'react'
import { FormGroup, FormControlStatic, ValueLine, ValueLineType, EntityLine, EntityCombo, EntityList, EntityRepeater } from '../../../../Framework/Signum.React/Scripts/Lines'
import { ModifiableEntity, External, JavascriptMessage, EntityControlMessage } from '../../../../Framework/Signum.React/Scripts/Signum.Entities'
import { classes, Dic } from '../../../../Framework/Signum.React/Scripts/Globals'
import * as Finder from '../../../../Framework/Signum.React/Scripts/Finder'
import { FindOptions } from '../../../../Framework/Signum.React/Scripts/FindOptions'
import { getQueryNiceName, MemberInfo, PropertyRoute, Binding } from '../../../../Framework/Signum.React/Scripts/Reflection'
import * as Navigator from '../../../../Framework/Signum.React/Scripts/Navigator'
import { TypeContext, FormGroupStyle } from '../../../../Framework/Signum.React/Scripts/TypeContext'
import Typeahead from '../../../../Framework/Signum.React/Scripts/Lines/Typeahead'
import { Expression, ExpressionOrValue, DesignerContext, DesignerNode } from './NodeUtils'
import { BaseNode, LineBaseNode } from './Nodes'
import * as NodeUtils from './NodeUtils'
import JavascriptCodeMirror from '../../Codemirror/JavascriptCodeMirror'
import { DynamicViewEntity, DynamicViewMessage } from '../Signum.Entities.Dynamic'
import { Modal, ModalProps, ModalClass, ButtonToolbar, Button } from 'react-bootstrap'
import { openModal, IModalProps } from '../../../../Framework/Signum.React/Scripts/Modals';
import TypeHelpComponent from '../Help/TypeHelpComponent'
import ValueLineModal from '../../../../Framework/Signum.React/Scripts/ValueLineModal'

export interface ExpressionOrValueProps {
    binding: Binding<any>;
    dn: DesignerNode<BaseNode>;
    refreshView?: () => void;
    type: "number" | "string" | "boolean" | "textArea" |  null;
    options?: (string | number)[] | ((query: string) => string[]);
    defaultValue: number | string | boolean | null;
    allowsExpression?: boolean;
    avoidDelete?: boolean;
    hideLabel?: boolean;
}

export class ExpressionOrValueComponent extends React.Component<ExpressionOrValueProps, void> {

    updateValue(value: string | boolean | undefined) {
        var p = this.props;

        var parsedValue = p.type != "number" ? value : (parseFloat(value as string) || null);

        if (parsedValue === "")
            parsedValue = null;

        if (parsedValue == p.defaultValue && !p.avoidDelete)
            p.binding.deleteValue();
        else
            p.binding.setValue(parsedValue);

        (p.refreshView || p.dn.context.refreshView)();
    }

    handleChangeCheckbox = (e: React.ChangeEvent<any>) => {
        var sender = (e.currentTarget as HTMLInputElement);
        this.updateValue(sender.checked);
    }

    handleChangeSelectOrInput = (e: React.ChangeEvent<any>) => {
        var sender = (e.currentTarget as HTMLSelectElement | HTMLInputElement);
        this.updateValue(sender.value);
    }

    handleTypeaheadSelect = (item: string) => {
        this.updateValue(item);
        return item;
    }

    handleToggleExpression = (e: React.MouseEvent<any>) => {
        e.preventDefault();
        e.stopPropagation();
        var p = this.props;
        var value = p.binding.getValue();

        if (value instanceof Object && (value as Object).hasOwnProperty("__code__"))
        {
            if (p.avoidDelete)
                p.binding.setValue(undefined);
            else
                p.binding.deleteValue();
        }
        else
            p.binding.setValue({ __code__: "" } as Expression<any>);

        (p.refreshView || p.dn.context.refreshView)();
    }

    render() {
        const p = this.props;
        const value = p.binding.getValue();
        
        const expr = value instanceof Object && (value as Object).hasOwnProperty("__code__") ? value as Expression<any> : null;

        const expressionIcon = this.props.allowsExpression != false && < i className={classes("fa fa-calculator fa-1 formula", expr && "active")} onClick={this.handleToggleExpression}></i>;


        if (!expr && p.type == "boolean") {


            if (p.defaultValue == null) {

                return (<div>
                    <label>
                        {expressionIcon}
                        <NullableCheckBox value={value}
                            onChange={newValue => this.updateValue(newValue)}
                            label={!this.props.hideLabel && this.renderMember(value)}
                            />
                    </label>
                </div>
                );
            } else {
                return (
                    <div>
                        <label>
                            {expressionIcon}
                            <input className="design-check-box"
                                type="checkbox"
                                checked={value == undefined ? this.props.defaultValue as boolean : value}
                                onChange={this.handleChangeCheckbox} />
                            {!this.props.hideLabel && this.renderMember(value)}
                        </label>
                    </div>
                );
            }
        }

        if (this.props.hideLabel) {
            return (
                <div className="form-inline">
                    {expressionIcon}
                    {expr ? this.renderExpression(expr, p.dn!) : this.renderValue(value)}
                </div>
            );
        }

        return (
            <div className="form-group">
                <label className="control-label">
                    { expressionIcon }
                    { this.renderMember(value) }
                </label>
                <div>
                    {expr ? this.renderExpression(expr, p.dn!) : this.renderValue(value)}
                </div>
            </div>
        );
    }

    renderMember(value: number | string | null | undefined): React.ReactNode | undefined {
      
        return (
            <span
                className={value === undefined ? "design-default" : "design-changed"}>
                {this.props.binding.member}
            </span>
        );
    }

    renderValue(value: number | string | null | undefined) {

        if (this.props.type == null)
            return <p className="form-control-static">{DynamicViewMessage.UseExpression.niceToString()}</p>;

        const val = value === undefined ? this.props.defaultValue : value;

        const style = this.props.hideLabel ? { display: "inline-block" } as React.CSSProperties : undefined;
        
        if (this.props.options) {
            if (typeof this.props.options == "function")
                return (
                    <div style={{ position: "relative" }}>
                        <Typeahead
                            inputAttrs={{ className: "form-control sf-entity-autocomplete" }}
                            getItems={this.handleGetItems}
                            onSelect={this.handleTypeaheadSelect} />
                    </div>
                );
                else
            return (
                <select className="form-control" style={style}
                    value={val == null ? "" : val.toString()} onChange={this.handleChangeSelectOrInput} >
                    {this.props.defaultValue == null && <option value="">{" - "}</option>}
                    {this.props.options.map((o, i) =>
                        <option key={i} value={o.toString()}>{o.toString()}</option>)
                    }
                </select>);
        }
        else {

            if (this.props.type == "textArea") {
                return (<textarea className="form-control" style={style}
                    type="text"
                    value={val == null ? "" : val.toString()}
                    onChange={this.handleChangeSelectOrInput} />);
            }

            return (<input className="form-control" style={style}
                type="text"
                value={val == null ? "" : val.toString()}
                onChange={this.handleChangeSelectOrInput} />);
        }
    }

    handleGetItems = (query: string) => {

        if (typeof this.props.options != "function")
            throw new Error("Unexpected options");

        const result = this.props.options(query);

        return Promise.resolve(result);
    }



    renderExpression(expression: Expression<any>, dn: DesignerNode<BaseNode>) {

        if (this.props.allowsExpression == false)
            throw new Error("Unexpected expression");

        const typeName = dn.parent!.fixRoute() !.typeReference().name.split(",").map(tn => tn.endsWith("Entity") ? tn : tn + "Entity").join(" | ");
        return (
            <div className="code-container">
                <pre style={{ border: "0px", margin: "0px" }}>{"(ctx: TypeContext<" + typeName + ">, auth) =>"}</pre>
                <JavascriptCodeMirror code={expression.__code__} onChange={newCode => { expression.__code__ = newCode; this.props.dn.context.refreshView() } } />
            </div>
        );
        
    }
}


interface NullableCheckBoxProps {
    label: React.ReactNode | undefined;
    value: boolean | undefined;
    onChange: (newValue: boolean | undefined) => void;
}

export class NullableCheckBox extends React.Component<NullableCheckBoxProps, void>{

    getIcon() {
        switch (this.props.value) {
            case true: return "glyphicon glyphicon-ok design-changed";
            case false: return "glyphicon glyphicon-remove design-changed";
            case undefined: return "glyphicon glyphicon-minus design-default"
        }
    }

    handleClick = (e: React.MouseEvent<any>) => {
        e.preventDefault();
        switch (this.props.value) {
            case true: this.props.onChange(false); break;
            case false: this.props.onChange(undefined); break;
            case undefined: this.props.onChange(true); break;
        }
    }

    render() {
        return (
            <a href="" onClick={this.handleClick}>
                <span className={this.getIcon()}/>
                {" "}
                {this.props.label}
            </a>
        );
    }
}

export interface FieldComponentProps  {
    dn: DesignerNode<BaseNode>,
    binding: Binding<string | undefined>,
}

export class FieldComponent extends React.Component<FieldComponentProps, void> {
    
    handleChange = (e: React.ChangeEvent<any>) => {
        var sender = (e.currentTarget as HTMLSelectElement);

        const node = this.props.dn.node;
        if (!sender.value)
            this.props.binding.deleteValue()
        else
            this.props.binding.setValue(sender.value);


        this.props.dn.context.refreshView();
    }
    
    render() {
        var p = this.props;
        var value = p.binding.getValue();
        
        return (
            <div className="form-group">
                <label className="control-label">
                    {p.binding.member}
                </label>
                <div>
                    {this.renderValue(value)}
                </div>
            </div>
        );
    }

    renderValue(value: string | null | undefined) {

        const strValue = value == null ? "" : value.toString();

        const route = this.props.dn.parent!.fixRoute();

        const subMembers = route ? route.subMembers() : {};

        return (<select className="form-control" value={strValue} onChange={this.handleChange} >
            <option value=""> - </option>
            {Dic.getKeys(subMembers).filter(k => subMembers[k].name != "Id").map((name, i) =>
                <option key={i} value={name}>{name}</option>)
            })
        </select>);
    }
}

export class DynamicViewInspector extends React.Component<{ selectedNode?: DesignerNode<BaseNode> }, void>{
    render() {

        const sn = this.props.selectedNode;

        if (!sn)
            return <h4>{DynamicViewMessage.SelectANodeFirst.niceToString()}</h4>;

        const error = NodeUtils.validate(sn, undefined);

        return (<div className="form-sm form-horizontal">
            <h4>
                {sn.node.kind}
                {sn.route && <small> ({Finder.getTypeNiceName(sn.route.typeReference())})</small>}
            </h4>
            {error && <div className="alert alert-danger">{error}</div>}
            {NodeUtils.renderDesigner(sn)}
        </div>);
    }
}


export interface CollapsableTypeHelpState{
    open: boolean;
}

export class CollapsableTypeHelp extends React.Component<{ initialTypeName?: string }, CollapsableTypeHelpState>{

    constructor(props: any) {
        super(props);
        this.state = { open: false };
    }

    handleHelpClick = (e: React.FormEvent<any>) => {
        e.preventDefault();
        this.setState({
            open: !this.state.open
        });
    }

    handleTypeHelpClick = (pr: PropertyRoute | undefined) => {
        if (!pr)
            return;

        ValueLineModal.show({
            type: { name: "string" },
            initialValue: TypeHelpComponent.getExpression("e", pr, "Typescript"),
            valueLineType: "TextArea",
            title: "Property Template",
            message: "Copy to clipboard: Ctrl+C, ESC",
            initiallyFocused: true,
        }).done();
    }

    render() {
        return (
            <div>
                <a href="#" onClick={this.handleHelpClick} className="design-help-button">
                    {this.state.open ?
                        DynamicViewMessage.HideHelp.niceToString() :
                        DynamicViewMessage.ShowHelp.niceToString()}
                </a>
                {this.state.open &&
                    <TypeHelpComponent
                        initialType={this.props.initialTypeName}
                        mode="Typescript"
                        onMemberClick={this.handleTypeHelpClick} />}
            </div>
        );
    }
}

interface DesignerModalProps extends React.Props<DesignerModal>, IModalProps {
    title: React.ReactNode;
    mainComponent: () => React.ReactElement<any>;
}

export class DesignerModal extends React.Component<DesignerModalProps, { show: boolean }>  {

    constructor(props: DesignerModalProps) {
        super(props);

        this.state = { show: true };
    }

    okClicked: boolean
    handleOkClicked = () => {
        this.okClicked = true;
        this.setState({ show: false });

    }

    handleCancelClicked = () => {
        this.setState({ show: false });
    }

    handleOnExited = () => {
        this.props.onExited!(this.okClicked);
    }

    render() {
        return <Modal bsSize="lg" onHide={this.handleCancelClicked} show={this.state.show} onExited={this.handleOnExited} className="sf-selector-modal">
            <Modal.Header closeButton={true}>
                <h4 className="modal-title">
                    {this.props.title}
                </h4>
                <ButtonToolbar>
                    <Button className="sf-entity-button sf-close-button sf-ok-button" bsStyle="primary" onClick={this.handleOkClicked}>{JavascriptMessage.ok.niceToString()}</Button>
                    <Button className="sf-entity-button sf-close-button sf-cancel-button" bsStyle="default" onClick={this.handleCancelClicked}>{JavascriptMessage.cancel.niceToString()}</Button>
                </ButtonToolbar>
            </Modal.Header>

            <Modal.Body>
                {this.props.mainComponent()}
            </Modal.Body>
        </Modal>;
    }

    static show(title: React.ReactNode, mainComponent: () => React.ReactElement<any>): Promise<boolean> {
        return openModal<boolean>(<DesignerModal title={title} mainComponent={mainComponent} />);
    }
}