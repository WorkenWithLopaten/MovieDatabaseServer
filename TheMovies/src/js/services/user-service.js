var userService = {

    getAllUsers() {
        let header = {};
        header["contentType"] = 'application/json';

        return requester.getSql('http://localhost:51443/api/users/get', header);
    },
    register(url, body) {
        let contentType = 'application/json';

        return requester.post(url, {}, body, contentType);
    }
};