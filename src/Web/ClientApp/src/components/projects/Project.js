import axios from "axios";

var _ = require('lodash');

export default {
    beforeRouteEnter : (to, from, next) => {
        if (to.name === "update-project") {
            axios
                .get("http://localhost:50598/api/projects/" + to.params.id)
                .then(response => {
                    if (!response.data) {
                        next('/not-found');
                    } else {
                        next(vm => {
                            vm.routingState = "update";
                            vm.params.range.data = {
                                start: new Date(response.data.startDateTime),
                                end: new Date(response.data.endDateTime),
                            };
                            vm.params.priority.data = response.data.priority.toString();
                            vm.params.projectName.data = response.data.projectName;
                            vm.params.customerCompany.data = response.data.customerCompany;
                            vm.params.performerCompany.data = response.data.performerCompany;
                            vm.params.projectManager.data = vm.parseSearchData(response.data.projectManager);
                            vm.params.performers.data = vm.parseSearchData(response.data.performers);

                            vm.id = to.params.id;
                            next();
                        })  
                    }
                })
                .catch(() => {
                    next('/not-found');
                });
        } else {
            next(vm => {
                vm.routingState = "create";
            }) 
        }
    },
    data() {
        return {
            routingState: null,
            id: null,
            params: {
                range: {
                    data: {
                        start: new Date('now'),
                        end: new Date(2021, 0, 0) 
                    },
                    error: false,
                    errorMessage: null,
                    validation: false
                },
                priority: {
                    data: "2",
                    error: false,
                    errorMessage: null,
                    validation: false
                },
                projectName: {
                    data: null,
                    error: false,
                    errorMessage: null,
                    validation: true
                },
                customerCompany: {
                    data: null,
                    error: false,
                    errorMessage: null,
                    validation: true
                },
                performerCompany: {
                    data: null,
                    error: false,
                    errorMessage: null,
                    validation: true
                },
                projectManager: {
                    data: null,
                    projectManagerOptions: [],
                    searchQuery: "",
                    error: false,
                    errorMessage: null,
                    validation: false
                },
                performers: {
                    data: null,
                    performerOptions: [],
                    searchQuery: "",
                    error: false,
                    errorMessage: null,
                    validation: false
                }, 
            },

            errors: false,
        }
    },
    methods: {
        getEmployeesList: function () {
            axios
                .get("http://localhost:50598/api/employees")
                .then(response => {
                    let data = this.parseSearchData(response.data);
                    
                    this.params.projectManager.projectManagerOptions = data;
                    this.params.performers.performerOptions = data;
                })
                .catch(err => {
                    // eslint-disable-next-line no-console
                    console.log(err);
                });
        },
        parseSearchData: function(response) {
            let data = [];
            
            if (response) {
                if (Array.isArray(response)) {
                    response.forEach((value) => {
                        let searchData = {
                            fullName: value['lastName'] + " " + value['firstName'] + " " + value['patronymicName'],
                            firstName: value['firstName'],
                            lastName: value['lastName'],
                            patronymicName: value['patronymicName'],
                            id: value['id'],
                        }

                        data.push(searchData)
                    })

                    return data;
                } else {
                    return {
                        fullName: response.lastName + " " + response.firstName + " " + response.patronymicName,
                        firstName: response.firstName,
                        lastName: response.lastName,
                        patronymicName: response.patronymicName,
                        id: response.id,
                    };
                }
            }
        },
        onChange: function (value) {
            _.debounce(function () { this.getEmployees(value); }, 300);
        },
        onOpen () {
            this.getEmployeesList();
        },
        async getEmployees(query) {
            axios
                .get("http://localhost:50598/api/employees/search", {
                    params: {
                        query: query,  
                    }
                })
                .then(response => {
                    let data = this.parseSearchData(response.data);

                    if (!Array.isArray(data)) {
                        data = [].push(data);
                    }
                    
                    this.params.projectManager.projectManagerOptions = data;
                    this.params.performers.performerOptions = data;
                })
                .catch(err => {
                    // eslint-disable-next-line no-console
                    console.log(err);
                });
        },
        submit: function () {
            if (this.validation() === true) {
                let data = {
                    projectName: this.params.projectName.data,
                    customerCompany: this.params.customerCompany.data,
                    performerCompany: this.params.performerCompany.data,
                    priority: this.params.priority.data,
                    projectManager: this.params.projectManager.data,
                    performers: this.params.performers.data,
                    startEndDate: {
                        start: this.params.range.data.start.toISOString(),
                        end: this.params.range.data.end.toISOString(),
                    },
                }
                if (this.routingState === "update") {
                    axios
                        .patch("http://localhost:50598/api/projects/" + this.id, data)
                        .then(() => {
                            this.$router.go();
                        })
                        .catch(err => {
                            // eslint-disable-next-line no-console
                            console.log(err);
                        });
                } else {
                    axios
                        .post("http://localhost:50598/api/projects", data)
                        .then(response => {
                            this.$router.push({ name: 'update-project', params: { id: response.data.id } })
                        })
                        .catch(err => {
                            // eslint-disable-next-line no-console
                            console.log(err);
                        });
                }
            }
            return false;
        },
        validation: function () {
            for (let param in this.params) {
                if (this.params[param]['validation'] === true && !this.params[param]['data']) {
                    this.errors = true;
                    this.params[param]['error'] = true;
                    this.params[param]['errorMessage'] = "This field can't be blank";
                }
            }
            
            return !this.errors;
        }
    }
}