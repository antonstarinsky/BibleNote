/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.8.2.0 (NJsonSchema v10.2.1.0 (Newtonsoft.Json v12.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable({
    providedIn: 'root'
})
export class NavigationProvidersClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : "";
    }

    getAll(): Observable<NavigationProvidersSharedViewModelsNavigationProviderVm[]> {
        let url_ = this.baseUrl + "/api/NavigationProviders/GetAll";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetAll(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetAll(<any>response_);
                } catch (e) {
                    return <Observable<NavigationProvidersSharedViewModelsNavigationProviderVm[]>><any>_observableThrow(e);
                }
            } else
                return <Observable<NavigationProvidersSharedViewModelsNavigationProviderVm[]>><any>_observableThrow(response_);
        }));
    }

    protected processGetAll(response: HttpResponseBase): Observable<NavigationProvidersSharedViewModelsNavigationProviderVm[]> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(NavigationProvidersSharedViewModelsNavigationProviderVm.fromJS(item));
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<NavigationProvidersSharedViewModelsNavigationProviderVm[]>(<any>null);
    }

    delete(id: number): Observable<void> {
        let url_ = this.baseUrl + "/api/NavigationProviders/Delete?";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined and cannot be null.");
        else
            url_ += "id=" + encodeURIComponent("" + id) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
            })
        };

        return this.http.request("delete", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processDelete(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processDelete(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processDelete(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return _observableOf<void>(<any>null);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<void>(<any>null);
    }

    getOneNoteProviderInfo(id: number): Observable<NavigationProvidersSharedViewModelsOneNoteNavigationProviderVm> {
        let url_ = this.baseUrl + "/api/NavigationProviders/GetOneNoteProviderInfo?";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined and cannot be null.");
        else
            url_ += "id=" + encodeURIComponent("" + id) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetOneNoteProviderInfo(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetOneNoteProviderInfo(<any>response_);
                } catch (e) {
                    return <Observable<NavigationProvidersSharedViewModelsOneNoteNavigationProviderVm>><any>_observableThrow(e);
                }
            } else
                return <Observable<NavigationProvidersSharedViewModelsOneNoteNavigationProviderVm>><any>_observableThrow(response_);
        }));
    }

    protected processGetOneNoteProviderInfo(response: HttpResponseBase): Observable<NavigationProvidersSharedViewModelsOneNoteNavigationProviderVm> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = NavigationProvidersSharedViewModelsOneNoteNavigationProviderVm.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<NavigationProvidersSharedViewModelsOneNoteNavigationProviderVm>(<any>null);
    }

    createOneNoteProvider(provider: NavigationProvidersSharedViewModelsOneNoteNavigationProviderVm): Observable<number> {
        let url_ = this.baseUrl + "/api/NavigationProviders/CreateOneNoteProvider";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(provider);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processCreateOneNoteProvider(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processCreateOneNoteProvider(<any>response_);
                } catch (e) {
                    return <Observable<number>><any>_observableThrow(e);
                }
            } else
                return <Observable<number>><any>_observableThrow(response_);
        }));
    }

    protected processCreateOneNoteProvider(response: HttpResponseBase): Observable<number> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = resultData200 !== undefined ? resultData200 : <any>null;
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<number>(<any>null);
    }

    updateOneNoteProvider(provider: NavigationProvidersSharedViewModelsOneNoteNavigationProviderVm): Observable<void> {
        let url_ = this.baseUrl + "/api/NavigationProviders/UpdateOneNoteProvider";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(provider);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
            })
        };

        return this.http.request("put", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processUpdateOneNoteProvider(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processUpdateOneNoteProvider(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processUpdateOneNoteProvider(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return _observableOf<void>(<any>null);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<void>(<any>null);
    }

    callOneNoteSelectHierarchyItemDialog(title: string | null, description: string | null, buttonText: string | null, callbackFunction: string | null): Observable<void> {
        let url_ = this.baseUrl + "/api/NavigationProviders/CallOneNoteSelectHierarchyItemDialog?";
        if (title === undefined)
            throw new Error("The parameter 'title' must be defined.");
        else if(title !== null)
            url_ += "title=" + encodeURIComponent("" + title) + "&";
        if (description === undefined)
            throw new Error("The parameter 'description' must be defined.");
        else if(description !== null)
            url_ += "description=" + encodeURIComponent("" + description) + "&";
        if (buttonText === undefined)
            throw new Error("The parameter 'buttonText' must be defined.");
        else if(buttonText !== null)
            url_ += "buttonText=" + encodeURIComponent("" + buttonText) + "&";
        if (callbackFunction === undefined)
            throw new Error("The parameter 'callbackFunction' must be defined.");
        else if(callbackFunction !== null)
            url_ += "callbackFunction=" + encodeURIComponent("" + callbackFunction) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processCallOneNoteSelectHierarchyItemDialog(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processCallOneNoteSelectHierarchyItemDialog(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processCallOneNoteSelectHierarchyItemDialog(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return _observableOf<void>(<any>null);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<void>(<any>null);
    }

    getOneNoteHierarchyItemInfo(hierarchyId: string | null): Observable<NavigationProvidersSharedViewModelsHierarchyItemVm> {
        let url_ = this.baseUrl + "/api/NavigationProviders/GetOneNoteHierarchyItemInfo?";
        if (hierarchyId === undefined)
            throw new Error("The parameter 'hierarchyId' must be defined.");
        else if(hierarchyId !== null)
            url_ += "hierarchyId=" + encodeURIComponent("" + hierarchyId) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetOneNoteHierarchyItemInfo(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetOneNoteHierarchyItemInfo(<any>response_);
                } catch (e) {
                    return <Observable<NavigationProvidersSharedViewModelsHierarchyItemVm>><any>_observableThrow(e);
                }
            } else
                return <Observable<NavigationProvidersSharedViewModelsHierarchyItemVm>><any>_observableThrow(response_);
        }));
    }

    protected processGetOneNoteHierarchyItemInfo(response: HttpResponseBase): Observable<NavigationProvidersSharedViewModelsHierarchyItemVm> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = NavigationProvidersSharedViewModelsHierarchyItemVm.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<NavigationProvidersSharedViewModelsHierarchyItemVm>(<any>null);
    }
}

export class NavigationProvidersSharedViewModelsNavigationProviderVm implements INavigationProvidersSharedViewModelsNavigationProviderVm {
    id?: number;
    name?: string | undefined;
    description?: string | undefined;
    isReadonly?: boolean;
    type?: BibleNoteDomainEnumsNavigationProviderType;

    constructor(data?: INavigationProvidersSharedViewModelsNavigationProviderVm) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
            this.description = _data["description"];
            this.isReadonly = _data["isReadonly"];
            this.type = _data["type"];
        }
    }

    static fromJS(data: any): NavigationProvidersSharedViewModelsNavigationProviderVm {
        data = typeof data === 'object' ? data : {};
        let result = new NavigationProvidersSharedViewModelsNavigationProviderVm();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["description"] = this.description;
        data["isReadonly"] = this.isReadonly;
        data["type"] = this.type;
        return data; 
    }

    clone(): NavigationProvidersSharedViewModelsNavigationProviderVm {
        const json = this.toJSON();
        let result = new NavigationProvidersSharedViewModelsNavigationProviderVm();
        result.init(json);
        return result;
    }
}

export interface INavigationProvidersSharedViewModelsNavigationProviderVm {
    id?: number;
    name?: string | undefined;
    description?: string | undefined;
    isReadonly?: boolean;
    type?: BibleNoteDomainEnumsNavigationProviderType;
}

export enum BibleNoteDomainEnumsNavigationProviderType {
    File = 1,
    Web = 2,
    OneNote = 3,
}

export class NavigationProvidersSharedViewModelsOneNoteNavigationProviderVm extends NavigationProvidersSharedViewModelsNavigationProviderVm implements INavigationProvidersSharedViewModelsOneNoteNavigationProviderVm {
    parameters?: BibleNoteProvidersOneNoteServicesNavigationProviderOneNoteNavigationProviderParameters | undefined;

    constructor(data?: INavigationProvidersSharedViewModelsOneNoteNavigationProviderVm) {
        super(data);
    }

    init(_data?: any) {
        super.init(_data);
        if (_data) {
            this.parameters = _data["parameters"] ? BibleNoteProvidersOneNoteServicesNavigationProviderOneNoteNavigationProviderParameters.fromJS(_data["parameters"]) : <any>undefined;
        }
    }

    static fromJS(data: any): NavigationProvidersSharedViewModelsOneNoteNavigationProviderVm {
        data = typeof data === 'object' ? data : {};
        let result = new NavigationProvidersSharedViewModelsOneNoteNavigationProviderVm();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["parameters"] = this.parameters ? this.parameters.toJSON() : <any>undefined;
        super.toJSON(data);
        return data; 
    }

    clone(): NavigationProvidersSharedViewModelsOneNoteNavigationProviderVm {
        const json = this.toJSON();
        let result = new NavigationProvidersSharedViewModelsOneNoteNavigationProviderVm();
        result.init(json);
        return result;
    }
}

export interface INavigationProvidersSharedViewModelsOneNoteNavigationProviderVm extends INavigationProvidersSharedViewModelsNavigationProviderVm {
    parameters?: BibleNoteProvidersOneNoteServicesNavigationProviderOneNoteNavigationProviderParameters | undefined;
}

export abstract class BibleNoteServicesNavigationProviderNavigationProviderParametersBase implements IBibleNoteServicesNavigationProviderNavigationProviderParametersBase {

    constructor(data?: IBibleNoteServicesNavigationProviderNavigationProviderParametersBase) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
    }

    static fromJS(data: any): BibleNoteServicesNavigationProviderNavigationProviderParametersBase {
        data = typeof data === 'object' ? data : {};
        throw new Error("The abstract class 'BibleNoteServicesNavigationProviderNavigationProviderParametersBase' cannot be instantiated.");
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        return data; 
    }

    clone(): BibleNoteServicesNavigationProviderNavigationProviderParametersBase {
        throw new Error("The abstract class 'BibleNoteServicesNavigationProviderNavigationProviderParametersBase' cannot be instantiated.");
    }
}

export interface IBibleNoteServicesNavigationProviderNavigationProviderParametersBase {
}

export class BibleNoteProvidersOneNoteServicesNavigationProviderOneNoteNavigationProviderParameters extends BibleNoteServicesNavigationProviderNavigationProviderParametersBase implements IBibleNoteProvidersOneNoteServicesNavigationProviderOneNoteNavigationProviderParameters {
    hierarchyItems?: BibleNoteProvidersOneNoteServicesModelsOneNoteHierarchyInfo[] | undefined;

    constructor(data?: IBibleNoteProvidersOneNoteServicesNavigationProviderOneNoteNavigationProviderParameters) {
        super(data);
    }

    init(_data?: any) {
        super.init(_data);
        if (_data) {
            if (Array.isArray(_data["hierarchyItems"])) {
                this.hierarchyItems = [] as any;
                for (let item of _data["hierarchyItems"])
                    this.hierarchyItems!.push(BibleNoteProvidersOneNoteServicesModelsOneNoteHierarchyInfo.fromJS(item));
            }
        }
    }

    static fromJS(data: any): BibleNoteProvidersOneNoteServicesNavigationProviderOneNoteNavigationProviderParameters {
        data = typeof data === 'object' ? data : {};
        let result = new BibleNoteProvidersOneNoteServicesNavigationProviderOneNoteNavigationProviderParameters();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        if (Array.isArray(this.hierarchyItems)) {
            data["hierarchyItems"] = [];
            for (let item of this.hierarchyItems)
                data["hierarchyItems"].push(item.toJSON());
        }
        super.toJSON(data);
        return data; 
    }

    clone(): BibleNoteProvidersOneNoteServicesNavigationProviderOneNoteNavigationProviderParameters {
        const json = this.toJSON();
        let result = new BibleNoteProvidersOneNoteServicesNavigationProviderOneNoteNavigationProviderParameters();
        result.init(json);
        return result;
    }
}

export interface IBibleNoteProvidersOneNoteServicesNavigationProviderOneNoteNavigationProviderParameters extends IBibleNoteServicesNavigationProviderNavigationProviderParametersBase {
    hierarchyItems?: BibleNoteProvidersOneNoteServicesModelsOneNoteHierarchyInfo[] | undefined;
}

export class BibleNoteProvidersOneNoteServicesModelsOneNoteHierarchyInfo implements IBibleNoteProvidersOneNoteServicesModelsOneNoteHierarchyInfo {
    id?: string | undefined;
    name?: string | undefined;
    type?: BibleNoteProvidersOneNoteServicesModelsOneNoteHierarchyType;

    constructor(data?: IBibleNoteProvidersOneNoteServicesModelsOneNoteHierarchyInfo) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
            this.type = _data["type"];
        }
    }

    static fromJS(data: any): BibleNoteProvidersOneNoteServicesModelsOneNoteHierarchyInfo {
        data = typeof data === 'object' ? data : {};
        let result = new BibleNoteProvidersOneNoteServicesModelsOneNoteHierarchyInfo();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["type"] = this.type;
        return data; 
    }

    clone(): BibleNoteProvidersOneNoteServicesModelsOneNoteHierarchyInfo {
        const json = this.toJSON();
        let result = new BibleNoteProvidersOneNoteServicesModelsOneNoteHierarchyInfo();
        result.init(json);
        return result;
    }
}

export interface IBibleNoteProvidersOneNoteServicesModelsOneNoteHierarchyInfo {
    id?: string | undefined;
    name?: string | undefined;
    type?: BibleNoteProvidersOneNoteServicesModelsOneNoteHierarchyType;
}

export enum BibleNoteProvidersOneNoteServicesModelsOneNoteHierarchyType {
    Notebook = 1,
    SectionGroup = 2,
    Section = 3,
    Page = 4,
}

export class NavigationProvidersSharedViewModelsHierarchyItemVm implements INavigationProvidersSharedViewModelsHierarchyItemVm {
    id?: string | undefined;
    name?: string | undefined;
    type?: BibleNoteProvidersOneNoteServicesModelsOneNoteHierarchyType;

    constructor(data?: INavigationProvidersSharedViewModelsHierarchyItemVm) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
            this.type = _data["type"];
        }
    }

    static fromJS(data: any): NavigationProvidersSharedViewModelsHierarchyItemVm {
        data = typeof data === 'object' ? data : {};
        let result = new NavigationProvidersSharedViewModelsHierarchyItemVm();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["type"] = this.type;
        return data; 
    }

    clone(): NavigationProvidersSharedViewModelsHierarchyItemVm {
        const json = this.toJSON();
        let result = new NavigationProvidersSharedViewModelsHierarchyItemVm();
        result.init(json);
        return result;
    }
}

export interface INavigationProvidersSharedViewModelsHierarchyItemVm {
    id?: string | undefined;
    name?: string | undefined;
    type?: BibleNoteProvidersOneNoteServicesModelsOneNoteHierarchyType;
}

export class SwaggerException extends Error {
    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isSwaggerException = true;

    static isSwaggerException(obj: any): obj is SwaggerException {
        return obj.isSwaggerException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    if (result !== null && result !== undefined)
        return _observableThrow(result);
    else
        return _observableThrow(new SwaggerException(message, status, response, headers, null));
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader();
            reader.onload = event => {
                observer.next((<any>event.target).result);
                observer.complete();
            };
            reader.readAsText(blob);
        }
    });
}