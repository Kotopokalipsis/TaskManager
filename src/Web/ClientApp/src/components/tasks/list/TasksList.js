import axios from "axios";

export default {
    data() {
        return {
            taskList: [],
            filters: {
                status: {
                    value: "4",
                },
                searchQuery: "",
            },
        }
    },
    mounted: function mounted () {
        this.getData();
    },
    methods: {
        getData() {
            axios
                .get("http://localhost:50598/api/tasks")
                .then(response => {
                    this.taskList = this.parseTasksFromResponse(response);
                })
                .catch(err => {
                    // eslint-disable-next-line no-console
                    console.log(err);
                });
        },
        parseTasksFromResponse(response) {
            let tasks = [];
            response.data.forEach((value) => {
                let task = {
                    id: value['id'],
                    comment: value['comment'],
                    taskName: value['taskName'],
                    priority: this.getPriorityStringByInt(value['priority']),
                    status: this.getStatusStringByInt(value['status']),
                    author: this.parseEmployerFromResponse(value['author']),
                    performer: this.parseEmployerFromResponse(value['performer']),
                }

                tasks.push(task);
            })
            
            return tasks;
        },
        getStatusStringByInt(value) {
            switch (value) {
                case 1: return "To Do";
                case 2: return "In Progress";
                case 3: return "Done";

                default: return "Unknown"
            }
        },
        parseEmployerFromResponse(value) {
            if (!value) {
                return {
                    fullName: "",
                    id: null
                };
            }
            
            return {
                fullName: value['lastName'] + ' ' + value['firstName'] + ' ' + value['patronymicName'],
                id: value['id']
            }
        },
        getPriorityStringByInt: function (value) {
            switch (value) {
                case 1: return "High";
                case 2: return "Normal";
                case 3: return "Low";
                
                default: return "Unknown"
            }
        },
        deleteTask(id) {
            axios
                .delete("http://localhost:50598/api/tasks/" + id)
                .then(() => {
                    this.$router.go();
                })
                .catch(err => {
                    // eslint-disable-next-line no-console
                    console.log(err);
                });
        },
        search() {
            var params = new URLSearchParams();
            
            if (this.filters.status.value !== "4") {
                params.append("status", this.filters.status.value);
            }
            
            if (this.filters.searchQuery) {
                params.append("taskName", this.filters.searchQuery);
            }
            
            axios
                .get("http://localhost:50598/api/tasks/search", {
                    params: params
                })
                .then(response => {
                    this.taskList = this.parseTasksFromResponse(response);
                })
                .catch(err => {
                    // eslint-disable-next-line no-console
                    console.log(err);
                });
        }
    }
}