﻿
import * as React from 'react'
import { Route } from 'react-router'
import { ajaxPost, ajaxGet } from '../../../Framework/Signum.React/Scripts/Services';
import { EntitySettings, ViewPromise } from '../../../Framework/Signum.React/Scripts/Navigator'
import * as Navigator from '../../../Framework/Signum.React/Scripts/Navigator'
import { EntityOperationSettings } from '../../../Framework/Signum.React/Scripts/Operations'
import { PseudoType, QueryKey } from '../../../Framework/Signum.React/Scripts/Reflection'
import * as Operations from '../../../Framework/Signum.React/Scripts/Operations'
import { CachePermission } from './Signum.Entities.Cache'
import * as OmniboxClient from '../Omnibox/OmniboxClient'
import * as AuthClient from '../Authorization/AuthClient'


export function start(options: { routes: JSX.Element[] }) {
    options.routes.push(<Route path="cache">
        <Route path="statistics" getComponent={(loc, cb) => require(["./CacheStatisticsPage"], (Comp) => cb(undefined, Comp.default))}/>
    </Route>);

    OmniboxClient.registerSpecialAction({
        allowed: () => AuthClient.isPermissionAuthorized(CachePermission.InvalidateCache),
        key: "ViewCache",
        onClick: () => Promise.resolve(Navigator.currentHistory.createHref("~/cache/statistics"))
    });
}


export module API {

    export function enable(): Promise<void> {
        return ajaxPost<void>({ url: "~/api/cache/enable" }, undefined);
    }

    export function disable(): Promise<void> {
        return ajaxPost<void>({ url: "~/api/cache/disable" }, undefined);
    }

    export function clear(): Promise<void> {
        return ajaxPost<void>({ url: "~/api/cache/clear" }, undefined);
    }

    export function view(): Promise<CacheState> {
        return ajaxGet<CacheState>({ url: "~/api/cache/view" });
    }
}


export interface CacheState {
    isEnabled: boolean;
    tables: CacheTableStats[];
    lazies: ResetLazyStats[];
}

export interface CacheTableStats {
    tableName: string;
    typeName: string;
    count: number;
    hits: number;
    invalidations: number;
    loads: number;
    sumLoadTime: string;
    subTables: CacheTableStats[];
}

export interface ResetLazyStats {
    typeName: string;
    hits: number;
    invalidations: number;
    loads: number;
    sumLoadTime: string;
}


