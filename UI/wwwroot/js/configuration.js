axios.defaults.baseURL = appConfig.apiBaseUrl;

// Add a request interceptor
axios.interceptors.request.use(config => {
    const token = localStorage.getItem('authToken');
    if (token) {
        config.headers['Authorization'] = 'Bearer ' + token;
    }
    config.headers['Content-Type'] = 'application/json';
    config.headers['Accept'] = 'application/json';
    config.headers['ApiKey'] = appConfig.apiKey;
    config.headers.common['Access-Control-Allow-Origin'] = '*';
    return config;
},
    error => {
        Promise.reject(error);
    });

//Add a response interceptor
axios.interceptors.response.use(response => {
    return response;
},
    error => {
        if (error.response) {
            if (error.response.status === 401) {
                notifyUnAuthenticatedError();
                window.location.href = appConfig.loginUrl;
                return Promise.resolve();
            }
            else if (error.response.status === 403) {
                notifyUnAuthorizedError();
                return Promise.resolve();
            }
            else {
                return Promise.reject(error);
            }
        }
        else {
            const errorMessage = "Something went wrong!";
            notifyGeneralError(errorMessage);
            return Promise.resolve();
        }
    });