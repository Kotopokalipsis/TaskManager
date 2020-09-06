import axios from "axios";
import { getEmployeesList, parseSearchData, getEmployees } from '../../statics/employees/EmployerManager.js'

var _ = require('lodash');

export default {
    beforeRouteEnter : (to, from, next) => {
        if (to.name === "update-task") {
            axios
                .get("http://localhost:50598/api/tasks/" + to.params.id)
                .then(response => {
                    if (!response.data) {
                        next('/not-found');
                    } else {
                        // eslint-disable-next-line no-console
                        console.log(response.data);
                        next(vm => {
                            vm.routingState = "update";
                            
                            vm.params.priority.data = response.data.priority.toString();
                            vm.params.status.data = response.data.status.toString();
                            vm.params.taskName.data = response.data.taskName;
                            vm.params.comment.data = response.data.comment;
                            vm.params.author.data = parseSearchData(response.data.author);
                            vm.params.performer.data = parseSearchData(response.data.performer);

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
                priority: {
                    data: "2",
                    error: false,
                    errorMessage: null,
                    validation: false
                },
                taskName: {
                    data: null,
                    error: false,
                    errorMessage: null,
                    validation: true
                },
                comment: {
                    data: null,
                    error: false,
                    errorMessage: null,
                    validation: true
                },
                author: {
                    data: null,
                    authorOptions: [],
                    searchQuery: "",
                    error: false,
                    errorMessage: null,
                    validation: false
                },
                performer: {
                    data: null,
                    performerOptions: [],
                    searchQuery: "",
                    error: false,
                    errorMessage: null,
                    validation: false
                },
                status: {
                    data: "1",
                    error: false,
                    errorMessage: null,
                    validation: false
                },
            },

            errors: false,
        }
    },
    methods: {
        async getEmployeesList() {
            let data = await getEmployeesList();
            
            this.params.author.authorOptions = data;
            this.params.performer.performerOptions = data;
        },
        onChange: function (value) {
            _.debounce(function () { this.getEmployees(value); }, 300);
        },
        onOpen () {
            this.getEmployeesList();
        },
        async getEmployees(query) {
            let data = await getEmployees(query);

            this.params.author.authorOptions = data;
            this.params.performer.performerOptions = data;
        },
        submit: function () {
            if (this.validation() === true) {
                let data = {
                    taskName: this.params.taskName.data,
                    comment: this.params.comment.data,
                    status: this.params.status.data,
                    priority: this.params.priority.data,
                    author: this.params.author.data,
                    performer: this.params.performer.data,
                }
                if (this.routingState === "update") {
                    axios
                        .patch("http://localhost:50598/api/tasks/" + this.id, data)
                        .then(() => {
                            this.$router.go();
                        })
                        .catch(err => {
                            // eslint-disable-next-line no-console
                            console.log(err);
                        });
                } else {
                    axios
                        .post("http://localhost:50598/api/tasks", data)
                        .then(response => {
                            this.$router.push({ name: 'update-task', params: { id: response.data.id } })
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