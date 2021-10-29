let authConfig = {
    authTokenkey: "authToken",
    authExpirykey: "authExpiry",
    userInfo: "userInfo",
    selectedValues: "selectedValues"
}

let permissionConfig = {
    "librarian": { "value": "Librarian", "label": "Librarian" },
    "libraryUser": { "value": "LibraryUser", "label": "Library User" },
}

function librarian(role) {
    role = role.toLowerCase().trim();
    return role === permissionConfig.librarian.label.toLowerCase();
}

function libraryUser(role) {
    role = role.toLowerCase().trim();
    return role === permissionConfig.libraryUser.label.toLowerCase();
}


function isAuthenticated() {
    const token = localStorage.getItem(authConfig.authTokenkey);
    const expiry = localStorage.getItem(authConfig.authExpirykey);

    if (!token || !expiry) {
        return false;
    }
    else if (new Date(parseInt(expiry)).getTime() < Date.now()) {

        return false;
    }
    else {
        return true;
    }
}

function getAuthToken() {
    const token = localStorage.getItem(authConfig.authTokenkey);
    return token;
}

function getUserInfo() {
    if (isAuthenticated()) {
        const userInfo = JSON.parse(localStorage.getItem(authConfig.userInfo));
        return userInfo;
    } else {
        return null;
    }
}

function setAuthInfo(authToken, authExpiry, userInfo) {
    localStorage.setItem(authConfig.authTokenkey, authToken);
    localStorage.setItem(authConfig.authExpirykey, authExpiry);
    const userInfoString = JSON.stringify(userInfo) || "";
    localStorage.setItem(authConfig.userInfo, userInfoString);
}

function clearAuthInfo() {
    localStorage.removeItem(authConfig.authTokenkey);
    localStorage.removeItem(authConfig.authExpirykey);
    localStorage.removeItem(authConfig.userInfo);
    localStorage.removeItem('selectedvalues');
}

async function login(email, password) {
    try {
        const data = { email: email, password };
        const response = await axios.post('/auth/token', data);
        const token = response.headers.token;
        const tokenExpiry = response.headers.tokenexpiry;
        const userInfo = {
            userId: response.data.data.id,
            email: response.data.data.email,
            firstName: response.data.data.firstName,
            lastName: response.data.data.lastName,
            roles: response.data.data.roles,
        }
        setAuthInfo(token, tokenExpiry, userInfo);
        updateUserInfoDisplay();
        notifyLoginSuccess();
        return true;

    } catch (error) {
        notifyLoginError(error.response.data.message);
        return false;
    }
}

async function resetPassword(resetPasswordInfo, url) {
    try {
        const data = resetPasswordInfo;
        const response = await axios.post(url, data);
        if (response) {
            notifyResetPasswordSuccess(response.data.message);
        }
    } catch (error) {
        notifyGeneralError(error.response.data.message);
    }
}

async function changePassword(changePasswordInfo, url) {
    try {
        const data = changePasswordInfo;
        const response = await axios.post(url, data);
        if (response) {
            notifyResetPasswordSuccess(response.data.message);
        }
    } catch (error) {
        notifyGeneralError(error.response.data.message);
    }
}



async function register(registrationInfo, url) {
    try {
        const data = registrationInfo;
        const response = await axios.post(url, data);
        if (response) {
            notifyRegistrationSuccess(response.data.message);
            return true;
        }
    } catch (error) {
        notifyGeneralError(error.response.data.message);
    }
}

async function update(updateInfo, url) {
    try {
        const data = updateInfo;
        const response = await axios.put(url, data);
        if (response) {
            notifyRegistrationSuccess(response.data.message);
        }
    } catch (error) {
        notifyGeneralError(error.response.data.message);
    }
}



function logout() {
    clearAuthInfo();
    window.location.href = appConfig.loginUrl;
}

function getDataList(obj, element, placeholder) {
    element.append(`<option value="">${placeholder}</option>`);
    $.each(obj, function (key, entry) {
        element.append($('<option></option>').attr('value', entry.id).text(entry.name));
    });
}

function updateUserInfoDisplay() {
    if (isAuthenticated()) {
        $("#userInfo").show();
        $("#userInfo").text(getUserInfo().firstName);
        $("#userProfileName").text(`${getUserInfo().firstName} ${getUserInfo().lastName}`);
        $("#userProfileEmail").text(`${getUserInfo().email}`);
        $("#roleUser").text(`${getUserInfo().roles}`);
        $("#logout").show();
    }
    else {
        $("#logout").hide();
        $("#userInfo").hide();
    }

}

function setSelectedValues(elementId, values) {
    if (values && values.length > 0) {
        $(`#${elementId}`).val(values);
    }
}

function updateSideMenu() {
    if (isIdhAdmin()) {
        /* $("#company-section").show();
         $("#company-section-menu").show();
         $("#produce-section").show();
         $("#produce-section-menu").show();
         $("#programme-section").show();
         $("#programme-section-menu").show();*/
        $("#country-section").show();
        $("#country-section-menu").show();
        $("#role-section").show();
        $("#role-section-menu").show();
        $("#form-section").show();
        $("#form-section-menu").show();

    }

    else {
        $("#company-section").hide();
        $("#company-section-menu").hide();
        $("#programme-section").hide();
        $("#programme-section-menu").hide();
        $("#produce-section").hide();
        $("#produce-section-menu").hide();
        $("#country-section").hide();
        $("#country-section-menu").hide();
        $("#role-section").hide();
        $("#role-section-menu").hide();
        $("#form-section").hide();
        $("#form-section-menu").hide();
    }
    if (isIdhAdmin() || isIdhProgramAdmin()) {
        $("#company-section").show();
        $("#company-section-menu").show();
        /*$("#programme-section").show();
        $("#programme-section-menu").show();*/
        $("#produce-section").show();
        $("#produce-section-menu").show();
    }
    else {
        $("#company-section").hide();
        $("#company-section-menu").hide();
        $("#programme-section").hide();
        $("#programme-section-menu").hide();
        $("#produce-section").hide();
        $("#produce-section-menu").hide();
    }
    if (isIdhAdmin() || isIdhProgramAdmin() || isIpAdmin()) {
        $("#user-section").show();
        $("#user-section-menu").show();
        $("#farmer-section-menu").show();
    }
    else {
        $("#user-section").hide();
        $("#user-section-menu").hide();
        $("#farmer-section-menu").hide();
    }
}

function isIdhAdmin() {
    const userRoles = getUserInfo().roles || [];
    const isIdhAdmin = userRoles.some(role => role.toLowerCase() === permissionConfig.idhAdmin.value.toLowerCase());
    return isIdhAdmin;
}

function isIdhProgramAdmin() {
    const userRoles = getUserInfo().roles || [];
    const isIdhProgramAdmin = userRoles.some(role => role.toLowerCase() === permissionConfig.idhProgramAdmin.value.toLowerCase());
    return isIdhProgramAdmin;
}
function isIdhViewer() {
    const userRoles = getUserInfo().roles || [];
    const isIdhViewer = userRoles.some(role => role.toLowerCase() === permissionConfig.idhViewer.value.toLowerCase());
    return isIdhViewer;
}

function isIpAdmin() {
    const userRoles = getUserInfo().roles || [];
    const isIpAdmin = userRoles.some(role => role.toLowerCase() === permissionConfig.ipAdmin.value.toLowerCase());
    return isIpAdmin;
}

function isIpViewer() {
    const userRoles = getUserInfo().roles || [];
    const isIpViewer = userRoles.some(role => role.toLowerCase() === permissionConfig.ipViewer.value.toLowerCase());
    return isIpViewer;
}

function isDataCollector() {
    const userRoles = getUserInfo().roles || [];
    const isDataCollector = userRoles.some(role => role.toLowerCase() === permissionConfig.dataCollector.value.toLowerCase());
    return isDataCollector;
}

function redirectfarmer() {
    const userId = localStorage.getItem("userInfo.userId");
    const url = `${appConfig.AppBaseUrl}/records/${userId}`;
    window.location(url);
}

function redirectchangePassword() {
    const id = localStorage.getItem("userInfo.userId");
    const url = `${appConfig.appBaseUrl}/change-password/${id}`;
    window.location(url);
}


async function getDataListForYears(element, placeholder) {
    const url = `${appConfig.apiBaseUrl}/programmes/years`;
    let response = await axios.get(url);
    let responsedata = response.data.years
    element.append(`<option value="">${placeholder}</option>`);
    function take(value, index, array) {
        element.append($('<option></option>').attr('value', value).text(value));
    }
    responsedata.forEach(take)
}