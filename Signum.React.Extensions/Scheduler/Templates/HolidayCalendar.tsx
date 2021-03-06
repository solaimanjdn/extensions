﻿import * as React from 'react'
import { classes } from '../../../../Framework/Signum.React/Scripts/Globals'
import { FormGroup, FormControlStatic, ValueLine, ValueLineType, EntityLine, EntityCombo, EntityDetail, EntityList, EntityRepeater} from '../../../../Framework/Signum.React/Scripts/Lines'
import {SearchControl }  from '../../../../Framework/Signum.React/Scripts/Search'
import { TypeContext, FormGroupStyle } from '../../../../Framework/Signum.React/Scripts/TypeContext'
import { HolidayCalendarEntity, HolidayEntity } from '../Signum.Entities.Scheduler'

export default class HolidayCalendar extends React.Component<{ ctx: TypeContext<HolidayCalendarEntity> }, void> {

    render() {
        
        const e = this.props.ctx;

        return (
            <div>    
                <ValueLine ctx={e.subCtx(f => f.name)}  />
                <div>
                    <EntityRepeater ctx={e.subCtx(f => f.holidays) } getComponent={(ctx: TypeContext<HolidayEntity>) => {
                        const ct4 = ctx.subCtx({ labelColumns: { sm: 4 } });
                        return (
                            <div className="row">
                                <div className="col-sm-4">
                                    <ValueLine ctx={ct4.subCtx(f => f.date) }  />
                                </div>
                                <div className="col-sm-4">
                                    <ValueLine ctx={ctx.subCtx(f => f.name) }  />
                                </div>
                            </div>);
                    }}  />
                </div>
            </div>
        );
    }
}

