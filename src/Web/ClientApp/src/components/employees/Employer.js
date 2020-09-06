import axios from "axios";

export default {
    beforeRouteEnter : (to, from, next) => {
        if (to.name === "update-employer") {
            axios
                .get("http://localhost:50598/api/employees/" + to.params.id)
                .then(response => {
                    if (!response.data) {
                        next('/not-found');
                    } else {
                        next(vm => {
                            vm.routingState = "update";
                            
                            vm.params.firstName.data = response.data.firstName;
                            vm.params.lastName.data = response.data.lastName;
                            vm.params.patronymicName.data = response.data.patronymicName;
                            vm.params.email.data = response.data.email;

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
            id: null,
            routingState: null,
            params: {
                firstName: {
                    data: null,
                    error: false,
                    errorMessage: null
                },
                lastName: {
                    data: null,
                    error: false,
                    errorMessage: null
                },
                patronymicName: {
                    data: null,
                    error: false,
                    errorMessage: null
                },
                email: {
                    data: null,
                    error: false,
                    errorMessage: null
                },
            },
            
            errors: false,
        }
    },
    methods: {
        submit: function () {
            if (this.validation() === true) {
                let data = {
                    firstName: this.params.firstName.data,
                    lastName: this.params.lastName.data,
                    patronymicName: this.params.patronymicName.data,
                    email: this.params.email.data,
                }
                if (this.routingState === "update") {
                    axios
                        .patch("http://localhost:50598/api/employees/" + this.id, data)
                        .then(() => {
                            this.$router.go();
                        })
                        .catch(err => {
                            // eslint-disable-next-line no-console
                            console.log(err);
                        });
                } else {
                    axios
                        .post("http://localhost:50598/api/employees", data)
                        .then(response => {
                            this.$router.push({name: 'update-employer', params: {id: response.data.id}})
                        })
                        .catch(err => {
                            // eslint-disable-next-line no-console
                            console.log(err);
                        });
                }
            }
        },
        validation: function () {
            for (let param in this.params) {
                if (!this.params[param]['data']) {
                    this.errors = true;
                    this.params[param]['error'] = true;
                    this.params[param]['errorMessage'] = "This field can't be blank";
                }
            }
            
            return !this.errors;
        }
    }
}