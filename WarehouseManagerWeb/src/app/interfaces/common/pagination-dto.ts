
export interface SearchRequest {
    advancedSearch?: AdvancedSearch;
    keyword?:        string;
    advancedFilter?: AdvancedFilter;
    pageNumber:     number;
    pageSize:       number;
    orderBy?:        string[];
}

export interface AdvancedFilter {
    logic:    string;
    filters:  string[];
    field:    string;
    operator: string;
    value:    string;
}

export interface AdvancedSearch {
    fields:  string[];
    keyword: string;
}
