import { HttpEvent, HttpHandlerFn, HttpRequest, HttpResponse } from "@angular/common/http";
import { environment } from "@environments/environment";
import { Observable, defer, from } from "rxjs";
import { map, switchMap, tap } from 'rxjs/operators';

const CACHE_NAME = 'precredit-api-cache';
const CACHEABLE_ENDPOINTS = [
    '/company/sizes',
    '/company/industries',
    '/company/main-usages',
    '/country',
    '/role/modules',
];


const CACHEABLE_URLS = CACHEABLE_ENDPOINTS.map(endpoint => `${environment.apiUrl}${endpoint}`);

export function cacheApiInterceptorFn(
    req: HttpRequest<unknown>,
    next: HttpHandlerFn
): Observable<HttpEvent<unknown>> {

    if (req.method !== 'GET' || !CACHEABLE_URLS.includes(req.url)) {
        return next(req);
    }

    return defer(() =>
        from(caches.match(req.urlWithParams))).pipe(
            switchMap(cachedResponse => {
                if (cachedResponse) {
                    return from(cachedResponse.json()).pipe(
                        map(body => new HttpResponse<any>({
                            status: 200,
                            body: body
                        }))
                    );
                }

                return next(req).pipe(
                    tap(event => {
                        if (event instanceof HttpResponse) {
                            caches.open(CACHE_NAME).then(cache => {
                                const responseToCache = new Response(JSON.stringify(event.body));
                                cache.put(req.urlWithParams, responseToCache);
                            });
                        }
                    })
                );
            })
        );
}