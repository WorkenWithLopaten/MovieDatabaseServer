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
    getJSON(url) {
        return new Promise((resolve, reject) => {
            $.ajax({
                url,
                method: "GET",
                contentType: "application/json",
                success(response) {
                    resolve(response);
                },
                error(response) {
                    console.log("error", response);

                }
            });
        });
    },
    putJSON(url, body, options) {
        options = options || {};
        return new Promise((resolve, reject) => {
            let headers = options.headers || {};

            $.ajax({
                url,
                headers,
                method: "PUT",
                contentType: "application/json",
                data: JSON.stringify(body),
                success(response) {
                    resolve(response);
                }
            });
        });
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