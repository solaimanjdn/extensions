﻿import * as React from 'react'
import { Route } from 'react-router'
import { Dic, classes } from '../../../Framework/Signum.React/Scripts/Globals';
import { Button, OverlayTrigger, Tooltip, MenuItem } from "react-bootstrap"
import { ajaxPost, ajaxPostRaw, ajaxGet, saveFile } from '../../../Framework/Signum.React/Scripts/Services';
import { EntitySettings, ViewPromise } from '../../../Framework/Signum.React/Scripts/Navigator'
import * as Constructor from '../../../Framework/Signum.React/Scripts/Constructor'
import * as Navigator from '../../../Framework/Signum.React/Scripts/Navigator'
import * as Finder from '../../../Framework/Signum.React/Scripts/Finder'
import { Lite, Entity, EntityPack, ExecuteSymbol, DeleteSymbol, ConstructSymbol_From, registerToString, JavascriptMessage, toLite } from '../../../Framework/Signum.React/Scripts/Signum.Entities'
import { EntityOperationSettings } from '../../../Framework/Signum.React/Scripts/Operations'
import { PseudoType, QueryKey, GraphExplorer, OperationType, Type, getTypeName } from '../../../Framework/Signum.React/Scripts/Reflection'
import * as Operations from '../../../Framework/Signum.React/Scripts/Operations'
import * as ContextualOperations from '../../../Framework/Signum.React/Scripts/Operations/ContextualOperations'
import { TimeSpanEntity, DateSpanEntity } from './Signum.Entities.Basics'
import * as OmniboxClient from '../Omnibox/OmniboxClient'
import * as AuthClient from '../Authorization/AuthClient'
import * as QuickLinks from '../../../Framework/Signum.React/Scripts/QuickLinks'

export function start(options: { routes: JSX.Element[] }) {
    Navigator.addSettings(new EntitySettings(TimeSpanEntity, e => new ViewPromise(resolve => require(['./Templates/TimeSpan'], resolve))));
    Navigator.addSettings(new EntitySettings(DateSpanEntity, e => new ViewPromise(resolve => require(['./Templates/DateSpan'], resolve))));
    Constructor.registerConstructor(TimeSpanEntity, () => TimeSpanEntity.New({ days: 0, hours: 0, minutes: 0, seconds: 0 }));
    Constructor.registerConstructor(DateSpanEntity, () => DateSpanEntity.New({ years: 0, months: 0, days: 0 }));
}
