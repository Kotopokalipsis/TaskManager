import axios from "axios";

export default {
    data() {
        return {
            employeesList: [],
        }
    },
    mounted: function mounted () {
        this.getData();
    },
    methods: {
        getData() {
            axios
                .get("http://localhost:50598/api/employees")
                .then(response => {
                    let employees = [];
                    response.data.forEach((value) => {
                        let employer = {
                            id: value['id'],
                            firstName: value['firstName'],
                            lastName: value['lastName'],
                            patronymicName: value['patronymicName'],
                            email: value['email'],
                        }

                        employees.push(employer);
                    })
                    this.employeesList = employees;
                })
                .catch(err => {
                    // eslint-disable-next-line no-console
                    console.log(err);
                });
        },
        deleteEmployer(id) {
            axios
                .delete("http://localhost:50598/api/employees/" + id)
                .then(() => {
                    this.$router.go();
                })
                .catch(err => {
                    // eslint-disable-next-line no-console
                    console.log(err);
                });
        }
    }
}