﻿
declare namespace BPMN {

    interface Options {
        container?: HTMLElement;
        width?: number | string;
        height?: number | string;
        moddleExtensions?: any;
        modules?: any[];
        additionalModules?: any[];
        keyboard?: {
            bindTo?: Node;
        }
    }

    interface SaveOptions {
        format?: boolean;
        preamble?: boolean;
    }

    interface Definitions {
    }

    interface Event {

    }

    interface DoubleClickEvent extends Event {
        element: DiElement;
        gfx: SVGElement;
        originalEvent: MouseEvent;

        type: string;
        stopPropagation(): void;
        preventDefault(): void;
    }

    interface AddClickEvent extends Event {
        element: DiElement;
    }

    interface PasteEvent extends Event {
        createdElements: { [oldId: string]: CreatedElement };
        descriptor: Descriptor;
    }

    interface CreatedElement {
        descriptor: Descriptor;
        element: DiElement;
    }

    interface Descriptor {
        id: string;
        name: string;
        type: string;
    }

    interface DiElement {
        attachers: any[];
        businessObject: ModdleElement;
        type: string;
        id: string;
        host: any;
        parent: DiElement;
        label: DiElement;
        colapsed: boolean;
        hidden: boolean;
        width: number;
        height: number;
        x: number;
        y: number;
        incoming: Connection[];
        outgoing: Connection[];
    }

    interface Connection extends DiElement {
        waypoints: DiElement[];
    }

    interface ModdleElement {
        id: string;
        parent: ModdleElement;
        di: DiElement;
        name: string;
        $type: string;
        lanes: ModdleElement[];
        eventDefinitions?: ModdleElement[];
    }

    interface ConnectionModdleElemnet extends ModdleElement {
        sourceRef: ModdleElement;
        targetRef: ModdleElement;
    }

    interface ElementRegistry {
        get(elementId: string): BPMN.DiElement;
        getAll(): BPMN.DiElement[];
        getGraphics(element: BPMN.DiElement): SVGElement;
        forEach(action: (element: BPMN.DiElement) => void): void;
    }

    interface GraphicsFactory {
        update(type: string, element: BPMN.DiElement, gfx: SVGElement): void;
    }

    interface BpmnFactory {
        create(type: string, attrs: any): ModdleElement;
    }

    interface EventBus {
        on(event: string, callback: (obj: BPMN.Event) => void, target?: BPMN.DiElement): void;
        on(event: string, priority: number, callback: (obj: BPMN.Event) => void, target?: BPMN.DiElement): void;
        off(event: string, callback: () => void): void;
    }

    interface Overlays {
        add(element: BPMN.DiElement, type: string, overlay: Overlay): void
        remove(condition: { type: string }) : void;
    }

    interface Overlay {
        position: RelativePosition;
        html: string;
    }

    interface RelativePosition {
        top?: number;
        bottom?: number;
        left?: number;
        right?: number 
    }
}

declare module 'bpmn-js/lib/Viewer' {

    class Viewer {
        _modules: any[];
        constructor(options: BPMN.Options)
        importXML(xml: string, done: (error: string, warning: string[]) => void): void;
        saveXML(options: BPMN.SaveOptions, done: (error: string, xml: string) => void): void;
        saveSVG(options: BPMN.SaveOptions, done: (error: string, svgStr: string) => void): void;
        importDefinitions(definition: BPMN.Definitions, done: (error: string) => void): void;
        getModules(): void;
        destroy(): void;
        on(event: string, callback: (obj: BPMN.Event) => void, target?: BPMN.DiElement): void;
        on(event: string, priority: number, callback: (obj: BPMN.Event) => void, target?: BPMN.DiElement): void;
        off(event: string, callback: () => void): void;
        get<T>(module: string): T;
        _emit(event: string, element: Object): void;
    }

    export = Viewer;
}

declare module 'bpmn-js/lib/NavigatedViewer' {

    import Viewer = require("bpmn-js/lib/Viewer");

    class NavigatedViewer extends Viewer {
      
    }

    export = NavigatedViewer;
}



declare module 'bpmn-js/lib/Modeler' {
    import Viewer = require("bpmn-js/lib/Viewer");

    class Modeler extends Viewer {
        createDiagram(done: (error: string, warning: string[]) => void): void;
    }

    export = Modeler
}

declare module 'bpmn-js/lib/draw/BpmnRenderer' {

    class BpmnRenderer {
        constructor(eventBus: BPMN.EventBus, styles: any, pathMap: any, canvas: any, priority: number);

        drawShape(visuals: any, element: BPMN.DiElement): SVGElement;
        drawConnection(visuals: any, element: BPMN.DiElement): SVGElement;
    }

    export = BpmnRenderer
}

declare module 'bpmn-js/lib/features/popup-menu/ReplaceMenuProvider' {

    class BpmnReplaceMenuProvider {
        constructor(popupMenu: any, modeling: any, moddle: BPMN.ModdleElement, bpmnReplace: any, rules: any, translate: any);

        _createMenuEntry(definition: any, element: BPMN.DiElement, action: any): any;
    }

    export = BpmnReplaceMenuProvider
}

declare module 'bpmn-js/lib/features/search' {
    var a : {};
    export = a;
}


