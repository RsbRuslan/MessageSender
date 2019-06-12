class HttpService {
    async post(url, data, callback){
        return await fetch(url, {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                'Content-type': 'application/json'
            }
        })
        .catch(error => console.log(error));
    }

    async get(url){
        return await fetch(url).catch(error => console.log(error));
    }
}
