var requester = {
    get(url) {
        return new Promise((resolve, reject) => {
            $.ajax({
                url,
                method: "GET",
                success(response) {

                    resolve(response);
                },
                error(response) {}
            });
        });
    },
    getSql(url, headers) {
        return requestSql(url, 'GET', headers);
    },
    post(url, headers, body, contentType) {
        return requestSql(url, 'POST', headers, JSON.stringify(body), contentType);
    }
};

function requestSql(url, type, headers, body, contentType) {
    const promise = new Promise((resolve, reject) => $.ajax({
        url,
        type,
        data: body,
        contentType,
        headers,
        success: resolve,
        error: reject
    }));

    return promise;
}