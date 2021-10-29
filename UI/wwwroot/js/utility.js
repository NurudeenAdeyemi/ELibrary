function emptyGuid() {
    return '00000000-0000-0000-0000-000000000000';
}
function getSelectFieldValues(element) {

    return element.map(function () { if ($(this).val() != "") return $(this).val(); }).get();

}

async function handleLoginForm(returnUrl) {

    let form = document.getElementById('loginForm');

    form.addEventListener('submit', async e => {
        e.preventDefault();

        const submitButton = document.getElementById('#loginSubmit');

        $(submitButton).attr('disabled', 'disabled');

        const email = $('#loginForm input[name=email]').val();

        const password = $('#loginForm input[name=password]').val();

        if (!email || !password) {

            notifyDataRequired('Email and password is required');
            $(submitButton).removeAttr('disabled');

        } else {

            const successful = await login(email, password, returnUrl);
            $(submitButton).removeAttr('disabled');

            if (successful) {
                if (returnUrl) {
                    window.location.href = returnUrl;
                } else {
                    window.location.href = appConfig.appBaseUrl;
                }
            }
        }
    });
}

async function handleForgotPasswordForm() {

    let form = document.getElementById('forgotPasswordForm');

    form.addEventListener('submit', async e => {
        e.preventDefault();
        const submitButton = $('forgotPasswordSubmit');
        // const submitButton = document.getElementById('#forgotPasswordSubmit');

        //$(submitButton).attr('disabled', 'disabled');
        //spinButton(submitButton);

        const url = `${appConfig.apiBaseUrl}/account/forgotpassword`;
        const email = $('#forgotPasswordForm input[name=email]').val();

        if (!email) {

            notifyDataRequired('Kindly complete the form correctly.');
            // stopButtonSpin(submitButton);
            form.reset();

        } else {

            const forgotPasswordInfo = {
                email
            }

            await register(forgotPasswordInfo, url).then((result) => {

                //stopButtonSpin(submitButton);
                form.reset();

            });

            //stopButtonSpin(submitButton);
            form.reset();
        }



    });
}

async function ResetPasswordForm() {


    let form = document.getElementById('resetPasswordForm');

    form.addEventListener('submit', async e => {

        e.preventDefault();

        const submitButton = $('#resetPasswordBtn');

        //spinButton(submitButton);

        const url = `${appConfig.apiBaseUrl}/account/resetpassword`;
        const params = new URLSearchParams(window.location.search);
        const id = params.get("id");
        const token = params.get("token");
        const password = $('#resetPasswordForm input[name=password]').val();
        const confirmPassword = $('#resetPasswordForm input[name=confirmPassword]').val();
        if (!password) {

            notifyDataRequired('Kindly complete the form correctly.');
            //stopButtonSpin(submitButton);
            form.reset();

        } else {
            if (confirmPassword != password) {

                notifyValueNotEqual("Password does not match!");

            }
            else {

                const resetPasswordInfo = {

                    id, token, password, confirmPassword
                }

                await resetPassword(resetPasswordInfo, url).then((result) => {

                    //stopButtonSpin(submitButton);
                    form.reset();

                });

                //stopButtonSpin(submitButton);
                form.reset();
            }
        }

    });
}
async function handleRegisterForm(returnUrl) {

    if (isIdhAdmin()) {
        getCompanies();
        getCompanyProgrammesAndCountries('companies', 'programmes', 'countries');
    }
    if (isIdhViewer()) {
        getCompanies();
        getCompanyProgrammesAndCountries('companies', 'programmes', 'countries');
    }
    if (isIdhProgramAdmin()) {
        getCompanies();
        getCompanyProgrammesAndCountries('companies', 'programmes', 'countries');
    }
    if (isIpAdmin()) {
        getCountries();
        getProgrammes();
    }
    if (isIpViewer()) {
        getCountries();
        getProgrammes();
    }

    let accessAllProgrammes = false;
    let accessAllProduces = false;
    let accessAllCountries = false;
    let accessAllCompanies = false;
    $('#adminProgramme').hide();
    $('#registerForm select[name="roles[]"]').on('change', async function () {
        var role = $(this).find("option:selected").text();
        if (role.toLowerCase() === permissionConfig.idhAdmin.label.toLowerCase()) {
            $('#registerCompany').hide();
            $('#registerCountry').hide();
            $('#registerProduce').hide();
            $('#registerProgramme').hide();
            $('#enumeratorCode').show();
            $('#adminProgramme').hide();
            accessAllCompanies = accessAllCountries = accessAllProduces = accessAllProgrammes = true;
        }
        else if (role.toLowerCase() === permissionConfig.idhViewer.label.toLowerCase()) {
            $('#registerCompany').hide();
            $('#registerCountry').hide();
            $('#registerProduce').hide();
            $('#registerProgramme').hide();
            $('#enumeratorCode').hide();
            $('#adminProgramme').hide();
            accessAllCompanies = accessAllCountries = accessAllProduces = accessAllProgrammes = true;
        }
        else if (role.toLowerCase() === permissionConfig.idhProgramAdmin.label.toLowerCase() && isIdhAdmin()) {
            $('#registerCompany').hide();
            $('#registerCountry').hide();
            $('#registerProduce').hide();
            $('#registerProgramme').hide();
            $('#enumeratorCode').show();
            $('#adminProgramme').show();
            await getProgrammes('programmes2');
            accessAllCompanies = accessAllProduces = false;
            accessAllProgrammes = false;
            accessAllCountries = true;
        }
        else if (role.toLowerCase() === permissionConfig.ipAdmin.label.toLowerCase() && isIdhAdmin()) {
            $('#registerCompany').show();
            $('#registerCountry').hide();
            $('#registerProduce').hide();
            $('#registerProgramme').hide();
            $('#enumeratorCode').show();
            $('#adminProgramme').hide();
            accessAllCompanies = accessAllCountries = accessAllProduces = accessAllProgrammes = true;
        }
        else if (role.toLowerCase() === permissionConfig.ipAdmin.label.toLowerCase() && isIpAdmin()) {
            $('#registerCompany').hide();
            $('#registerCountry').hide();
            $('#registerProduce').hide();
            $('#registerProgramme').hide();
            $('#enumeratorCode').show();
            $('#adminProgramme').hide();
            accessAllCompanies = accessAllCountries = accessAllProduces = accessAllProgrammes = true;
        }
        else if (role.toLowerCase() === permissionConfig.ipAdmin.label.toLowerCase() && isIdhProgramAdmin()) {
            $('#registerCompany').show();
            $('#registerCountry').hide();
            $('#registerProduce').hide();
            $('#registerProgramme').hide();
            $('#enumeratorCode').show();
            $('#adminProgramme').hide();
            accessAllCompanies = accessAllCountries = accessAllProduces = accessAllProgrammes = true;
        }
        else if (role.toLowerCase() === permissionConfig.ipViewer.label.toLowerCase() && isIdhAdmin()) {
            $('#registerCompany').show();
            $('#registerCountry').hide();
            $('#registerProduce').hide();
            $('#registerProgramme').hide();
            $('#enumeratorCode').hide();
            $('#adminProgramme').hide();
            accessAllCompanies = accessAllCountries = accessAllProduces = accessAllProgrammes = true;
        }
        else if (role.toLowerCase() === permissionConfig.ipViewer.label.toLowerCase() && isIdhProgramAdmin()) {
            $('#registerCompany').show();
            $('#registerCountry').hide();
            $('#registerProduce').hide();
            $('#registerProgramme').hide();
            $('#enumeratorCode').hide();
            $('#adminProgramme').hide();
            accessAllCompanies = accessAllCountries = accessAllProduces = accessAllProgrammes = true;
        }
        else if (role.toLowerCase() === permissionConfig.ipViewer.label.toLowerCase() && isIpAdmin()) {
            $('#registerCompany').hide();
            $('#registerCountry').hide();
            $('#registerProduce').hide();
            $('#registerProgramme').hide();
            $('#enumeratorCode').hide();
            $('#adminProgramme').hide();
            accessAllCompanies = accessAllCountries = accessAllProduces = accessAllProgrammes = true;
        }
        else if (role.toLowerCase() === permissionConfig.dataCollector.label.toLowerCase() && isIdhAdmin()) {
            $('#registerCompany').show();
            $('#registerCountry').show();
            $('#registerProduce').show();
            $('#registerProgramme').show();
            $('#enumeratorCode').show();
            $('#adminProgramme').hide();
            accessAllCompanies = accessAllCountries = accessAllProduces = accessAllProgrammes = true;
        }
        else if (role.toLowerCase() === permissionConfig.dataCollector.label.toLowerCase() && isIdhProgramAdmin()) {
            $('#registerCompany').show();
            $('#registerCountry').show();
            $('#registerProduce').show();
            $('#registerProgramme').show();
            $('#enumeratorCode').show();
            $('#adminProgramme').hide();
            accessAllCompanies = accessAllCountries = accessAllProduces = accessAllProgrammes = true;
        }
        else if (role.toLowerCase() === permissionConfig.dataCollector.label.toLowerCase() && isIpAdmin()) {
            $('#registerCompany').hide();
            $('#registerCountry').show();
            $('#registerProduce').show();
            $('#registerProgramme').show();
            $('#enumeratorCode').show();
            $('#adminProgramme').hide();
            accessAllCompanies = accessAllCountries = accessAllProduces = accessAllProgrammes = false;
        }
        else {
            $('#registerCompany').show();
            $('#registerCountry').show();
            $('#registerProduce').show();
            $('#registerProgramme').show();
            $('#enumeratorCode').show();
            $('#adminProgramme').hide();
            accessAllCompanies = accessAllCountries = accessAllProduces = accessAllProgrammes = false;
        }
    });
    let form = document.getElementById('registerForm');

    form.addEventListener('submit', async e => {

        e.preventDefault();

        let submitButton = $('#registerSubmit');

        submitButton.attr('disabled', 'disabled');

        const url = '/account/register';
        const userId = getUserInfo().userId;
        //const userId = localStorage.getItem(authConfig.userInfo.userId);

        const viewUrl = `/records/${userId}`;

        const email = $('#registerForm input[name=email]').val();

        const password = $('#registerForm input[name=password]').val();

        const firstName = $('#registerForm input[name=firstname]').val();

        const lastName = $('#registerForm input[name=lastname]').val();

        const phone = $('#registerForm input[name=phone]').val();

        const enumeratorCode = $('#registerForm input[name=enumeratorCode]').val();

        const cpassword = $('#registerForm input[name=cpassword]').val();

        const roles = getSelectFieldValues($('#registerForm select[name="roles[]"]'));

        const companies = getSelectFieldValues($('#registerForm select[name="companyId[]"]'));

        const countries = getSelectFieldValues($('#registerForm select[name="countries[]"]'));

        const produces = getSelectFieldValues($('#registerForm select[name="produces[]"]'));
        const roleText = $("#roles option:selected").text();
        let programmes = [];
        if (roleText.toLowerCase() === permissionConfig.idhProgramAdmin.label.toLowerCase() && isIdhAdmin()) {
            programmes = getSelectFieldValues($('#registerForm select[name="programmes2[]"]'));
        }
        else {
            programmes = getSelectFieldValues($('#registerForm select[name="programmes[]"]'));
        }

        if (!email || !password || !firstName || !lastName) {

            notifyDataRequired('Kindly complete the form correctly.');
            submitButton.removeAttr('disabled');
            //form.reset();

        } else {

            if (cpassword != password) {

                notifyValueNotEqual("Password does not match!");

            } else {
                let companyId = emptyGuid();
                if (!isIdhRole(roleText) && companies && companies.length > 0) {
                    companyId = companies[0];
                }

                const registrationInfo = {

                    email, password, phone, firstName, lastName, roles, companyId, countries, produces, programmes, enumeratorCode,
                    accessAllCompanies, accessAllCountries, accessAllProduces, accessAllProgrammes
                }

                await register(registrationInfo, url, returnUrl).then((result) => {

                    submitButton.removeAttr('disabled');
                    //form.reset();
                    if (result) {
                        if (returnUrl) {
                            window.location.href = returnUrl;
                        } else {
                            window.location.href = viewUrl;
                        }
                    }
                });
            }

            submitButton.removeAttr('disabled');
            //form.reset();
        }

    });
}




async function getFormTypesWithForms() {
    const response = await axios.get(`/formtypes`);
    return response.data.data;
}

async function getProgrammeFormsFromFromTypes() {
    let form = await getFormTypesWithForms();
    let content = '';
    form.forEach(f => {
        content += `<div class="col-md-12 form-check">
                                <label class="form-check-label text-primary font-weight-boldest"> 
                                     ${f.name}
                                </label><fieldset>`;

        f.forms.forEach(fm => {
            const radioInput = `<div class="form-check-label"> <input type="radio" class="form-check-input" value="${fm.id}" name="${f.name}">${fm.name}</div><p class="font-italic font-size-sm text-info">*${fm.description}</p>`
            content += radioInput;
        });

        content += `</fieldset></div>`;
    });


    let forms = document.getElementById('formtypeforms');
    forms.innerHTML = content;

}
async function generateKpis() {
    let kpisConfig = await getKpiConfigs(true);
    let content = '';
    kpisConfig.forEach(config => {
        content += `

   <div class="accordion" id="accordionExample">
                            <div id="accordionExample" class="accordion border-radius mt-4">
                                <div class="card border-none shadow-sm border-radius my-6">
                                    <div id="headingOne"
                                         class="bg-idh card-header bg-white  border-none border-radius px-5 py-3">

                                        <div class="d-flex">
                                            <h2 type="button" data-toggle="collapse"
                                                data-target="#${config.id}collapseTwo" aria-expanded="true"
                                                aria-controls="collapseTwo"
                                                class="w-75 d-flex btn btn-link text-white font-weight-bold  ">
                                                ${config.title}
                                            </h2>
                                            <div class="ml-auto d-flex py-1  card-toolbar">
                                                <div class="ml-auto dropdown dropdown-inline mr-5">
                                                    <button type="button"
                                                            class="btn btn-light-primary font-weight-bolder dropdown-toggle"
                                                            data-toggle="dropdown" aria-haspopup="true"
                                                            aria-expanded="false">
                                                        <span class="svg-icon svg-icon-md">
                                                            <!--begin::Svg Icon | path:assets/media/svg/icons/Design/PenAndRuller.svg-->
                                                            <svg xmlns="http://www.w3.org/2000/svg"
                                                                 xmlns:xlink="http://www.w3.org/1999/xlink"
                                                                 width="24px" height="24px"
                                                                 viewBox="0 0 24 24" version="1.1">
                                                                <g stroke="none" stroke-width="1"
                                                                   fill="none" fill-rule="evenodd">
                                                                    <rect x="0" y="0" width="24"
                                                                          height="24" />
                                                                    <path d="M3,16 L5,16 C5.55228475,16 6,15.5522847 6,15 C6,14.4477153 5.55228475,14 5,14 L3,14 L3,12 L5,12 C5.55228475,12 6,11.5522847 6,11 C6,10.4477153 5.55228475,10 5,10 L3,10 L3,8 L5,8 C5.55228475,8 6,7.55228475 6,7 C6,6.44771525 5.55228475,6 5,6 L3,6 L3,4 C3,3.44771525 3.44771525,3 4,3 L10,3 C10.5522847,3 11,3.44771525 11,4 L11,19 C11,19.5522847 10.5522847,20 10,20 L4,20 C3.44771525,20 3,19.5522847 3,19 L3,16 Z"
                                                                          fill="#000000"
                                                                          opacity="0.3" />
                                                                    <path d="M16,3 L19,3 C20.1045695,3 21,3.8954305 21,5 L21,15.2485298 C21,15.7329761 20.8241635,16.200956 20.5051534,16.565539 L17.8762883,19.5699562 C17.6944473,19.7777745 17.378566,19.7988332 17.1707477,19.6169922 C17.1540423,19.602375 17.1383289,19.5866616 17.1237117,19.5699562 L14.4948466,16.565539 C14.1758365,16.200956 14,15.7329761 14,15.2485298 L14,5 C14,3.8954305 14.8954305,3 16,3 Z"
                                                                          fill="#000000" />
                                                                </g>
                                                            </svg>
                                                            <!--end::Svg Icon-->
                                                        </span>Export
                                                    </button>
                                                    <!--begin::Dropdown Menu-->
                                                    <div class="dropdown-menu dropdown-menu-sm dropdown-menu-right">
                                                        <!--begin::Navigation-->
                                                        <ul class="navi flex-column navi-hover py-2">
                                                            <li class="navi-header font-weight-bolder text-uppercase font-size-sm text-primary pb-2">
                                                                Choose an option:
                                                            </li>
                                                            <li class="navi-item">
                                                                <a href="javascript:void(0);" onclick="fileExportClicked('${config.id}', 'excel')" class="navi-link">
                                                                    <span class="navi-icon">
                                                                        <i class="la la-file-excel-o"></i>
                                                                    </span>
                                                                    <span class="navi-text">Excel</span>
                                                                </a>
                                                            </li>
                                                            <li class="navi-item">
                                                                <a href="javascript:void(0);" onclick="fileExportClicked('${config.id}', 'csv')" class="navi-link">
                                                                    <span class="navi-icon">
                                                                        <i class="la la-file-text-o"></i>
                                                                    </span>
                                                                    <span class="navi-text">CSV</span>
                                                                </a>
                                                            </li>
                                                            <li class="navi-item">
                                                                <a href="javascript:void(0);" onclick="fileExportClicked('${config.id}', 'json')" class="navi-link">
                                                                    <span class="navi-icon">
                                                                        <i class="la la-file-json-o"></i>
                                                                    </span>
                                                                    <span class="navi-text">JSON</span>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                        <!--end::Navigation-->
                                                    </div>
                                                    <!--end::Dropdown Menu-->
                                                </div>
                                                 
                                                <button type="button" data-toggle="collapse"
                                                        data-target="#${config.id}collapseTwo" aria-expanded="false"
                                                        aria-controls="collapseTwo"
                                                        class=" d-flex btn btn-link collapsible-link ml-5 ">
                                                </button>
                                            </div>

                                        </div>

                                    </div>
                                    <div>
                                        <div id="${config.id}collapseTwo" aria-labelledby="headingOne"
                                         data-parent="#accordionExample"
                                         class="border-radius-half chart-port">
                                        <div class="card-body p-5">

                                            <div class="panel-body">

                                                <div id="kpichart${config.id}" class="ct-chart"></div>
                                                <div id="${config.id}" class="table-responsive"></div>
                                            </div>
                                        </div>
                                    </div>
                                    </div>
                                </div>
                            </div>
                        </div>`;
    });

    let kpiDashboard = document.getElementById('kpidashboard');
    kpiDashboard.innerHTML = content;
    let produces = getSelectedValues('userProduce') || [];
    let companies = getSelectedValues('userCompany') || [];
    let countries = getSelectedValues('userCountry') || [];
    let programmes = getSelectedValues('userProgramme') || [];
    let years = getSelectedValues('userYear') || [];
    let quarters = getSelectedValues('userQuarter') || [];

    kpisConfig.forEach(async config => {
        await bindKpiToHtml(config, produces, countries, companies, programmes, years, quarters);
    });

}
$(function () {
    setTimeout(function () {
        $('.chart-port').each(function () {

            $(this).addClass("collapse")
        })
        //$('.chart-port').on('shown.bs.collapse', function () {
        //    var elem = $(this)
        //    setTimeout(function () {

        //        alert('hey');
        //        alert(elem.prop('id'))
        //        generateKpis()
        //        //if (MyNewChart == null)
        //        //    MyNewChart = new Chart(ctx).Bar(data);
        //    }, 200);
        //})

    }, 2000);


}


)


function fileExportClicked(kpi, format) {
    let produces = getSelectedValues('userProduce') || [];
    let companies = getSelectedValues('userCompany') || [];
    let countries = getSelectedValues('userCountry') || [];
    let programmes = getSelectedValues('userProgramme') || [];
    let years = getSelectedValues('userYear') || [];
    let quarters = getSelectedValues('userQuarter') || [];
    exportKpi(kpi, format, produces, countries, companies, programmes, years, quarters);
}

async function bindKpiToHtml(config, produces, countries, companies, programmes, years, quarters) {

    produces = produces || [];
    countries = countries || [];
    companies = companies || [];
    programmes = programmes || [];
    years = years || [];
    quarters = quarters || [];
    let content = await getKpi(config.id, produces, countries, companies, programmes, years, quarters);
    document.getElementById(`${config.id}`).innerHTML = content.htmlContent;
    bindChart(config, content.jsonData);
}

function bindChart(config, jsonData) {
    let chartElem = $(`#kpichart${config.id}`);
    chartElem.nrecoPivotChart({
        pivotData: JSON.parse(jsonData),
        chartType: 'bar',
        chartOptions: {
            height: 270,
            plugins: [
                // optional chart plugin: tooltip on mouse over
                Chartist.plugins.tooltip({
                    tooltipOffset: { x: 0, y: -10 },
                    metaIsHTML: true,
                    appendToBody: true
                })
            ],
            plugins: [
                Chartist.plugins.tooltip({
                    appendToBody: true,
                    metaIsHTML: true,
                    pointClass: 'point-left',
                    class: 'tooltip-left',
                    tooltipOffset: {
                        x: +100,
                        y: -20,
                    }
                }),
                /*Chartist.plugins.tooltip({
                    appendToBody: true,
                    metaIsHTML: true,
                    pointClass: 'point-right',
                    class: 'tooltip-right',
                    tooltipOffset: {
                        x: -100,
                        y: -20,
                    },
                })*/
            ]
        }
    });
}

function bindUserProducesTab(selectedFilter) {
    let content = '';
    let produces = getUserInfo().produces || [];
    produces.forEach((produce, index) => {
        //content += `
        //        <li class="nav-item">
        //            <a class="d-grid px-5 nav-link active font-14" data-toggle="tab"
        //               href="#kt_tab_${produce.id}">
        //                <span class="w-fit-content mx-auto nav-icon">
        //                    <img alt="${produce.name}"
        //                         src="/assets/media/logos/${produce.name.toLowerCase()}.png" />
        //                </span>
        //                <span class="nav-text text-center">${produce.name}</span>
        //            </a>
        //        </li>`;


    });

    //document.getElementById(selectedFilter).innerHTML = content;
}

function getSelectedValues(elementId) {
    var val = $(`#${elementId}`).val();
    if (val) {
        return val.split(',');
    }
    return [];
}

function handleSelectionChange() {
    let produces = getSelectedValues('userProduce') || [];
    let companies = getSelectedValues('userCompany') || [];
    let countries = getSelectedValues('userCountry') || [];
    let programmes = getSelectedValues('userProgramme') || [];
    let years = getSelectedValues('userYear') || [];
    let quarters = getSelectedValues('userQuarter') || [];
    let selectedValues = {
        produces,
        companies,
        countries,
        programmes,
        years,
        quarters
    }
    localStorage.setItem(authConfig.selectedValues, JSON.stringify(selectedValues));
    generateKpis();
}

