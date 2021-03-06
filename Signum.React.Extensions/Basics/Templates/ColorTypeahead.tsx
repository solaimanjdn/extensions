﻿
import * as React from 'react'
import { Tab, Tabs } from 'react-bootstrap'
import { classes, Dic } from '../../../../Framework/Signum.React/Scripts/Globals'
import { FormGroup, FormControlStatic, ValueLine, ValueLineType, EntityLine, EntityCombo, EntityDetail, EntityList, EntityRepeater, EntityTabRepeater } from '../../../../Framework/Signum.React/Scripts/Lines'
import { SubTokensOptions, QueryToken, QueryTokenType, hasAnyOrAll } from '../../../../Framework/Signum.React/Scripts/FindOptions'
import Typeahead  from '../../../../Framework/Signum.React/Scripts/Lines/Typeahead'
import { SearchControl } from '../../../../Framework/Signum.React/Scripts/Search'
import { getToString, getMixin } from '../../../../Framework/Signum.React/Scripts/Signum.Entities'
import { TypeContext, FormGroupStyle } from '../../../../Framework/Signum.React/Scripts/TypeContext'
import { namedColors } from '../Color'


export class ColorTypeaheadLine extends React.Component<{ ctx: TypeContext<string | null | undefined>; onChange?: () => void }, void>{

    render() {
        var ctx = this.props.ctx;

        return (
            <FormGroup ctx={ctx} labelText={ctx.niceName()} >
                <ColorTypeahead color={ctx.value} onChange={newColor => {
                    ctx.value = newColor;
                    if (this.props.onChange)
                        this.props.onChange();
                    this.forceUpdate();
                }} />
            </FormGroup>
        );
    }
}

export class ColorTypeahead extends React.Component<{ color: string | null | undefined, onChange: (newColor: string | null | undefined) => void }, void>{

    handleGetItems = (query: string) => {
        if (!query)
            return Promise.resolve([
                "black",
                "#00000"
            ]);

        const result = Dic.getKeys(namedColors)
            .filter(k => k.toLowerCase().contains(query.toLowerCase()))
            .orderBy(a => a.length)
            .filter((k, i) => i < 5);

        if (result.length == 0) {
            if (query.match(/^(#[0-9A-F]{3}|#[0-9A-F]{6}|#[0-9A-F]{8})$/i))
                result.push(query);
        }

        return Promise.resolve(result);
    }

    handleSelect = (item: string) => {
        this.props.onChange(item);
        this.forceUpdate();
        return item;
    }

    handleRenderItem = (item: string, query: string) => {

        return (
            <span>
                <span className="icon fa fa-square" style={{ color: item }} />
                {Typeahead.highlightedText(item, query)}
            </span>
        );
    }

    render() {
        return (
            <div style={{ position: "relative" }}>
                <Typeahead
                    value={this.props.color || ""}
                    inputAttrs={{ className: "form-control sf-entity-autocomplete" }}
                    getItems={this.handleGetItems}
                    onSelect={this.handleSelect}
                    onChange={this.handleSelect}
                    renderItem={this.handleRenderItem}
                    minLength={0}
                    />
            </div>
        );
    }
}



