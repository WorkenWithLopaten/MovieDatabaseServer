let templates = {
    get(name) {
        let url = `src/templates/${name}.html`;
        return requester.get(url);
    }
};