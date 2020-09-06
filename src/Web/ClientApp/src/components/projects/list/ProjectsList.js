import axios from "axios";

export default {
    data() {
        return {
            projectsList: [],
            filters: {
                priority: {
                    high: {
                        value: 1,
                        enable: false
                    },
                    normal: {
                        value: 2,
                        enable: false
                    },
                    low: {
                        value: 3,
                        enable: false
                    },
                },
                startEndDate: {},
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
                .get("http://localhost:50598/api/projects")
                .then(response => {
                    this.projectsList = this.parseProjectsFromResponse(response);
                })
                .catch(err => {
                    // eslint-disable-next-line no-console
                    console.log(err);
                });
        },
        parseProjectsFromResponse(response) {
            let projects = [];
            response.data.forEach((value) => {
                let performers = [];
                if (value['performers']) {
                    value['performers'].forEach((performer) => {
                        performers.push(this.parseEmployerFromResponse(performer));
                    })
                }

                let project = {
                    id: value['id'],
                    customerCompany: value['customerCompany'],
                    projectName: value['projectName'],
                    priority: this.getPriorityStringByInt(value['priority']),
                    performerCompany: value['performerCompany'],
                    startDateTime: value['startDateTime'],
                    endDateTime: value['endDateTime'],
                    projectManager: this.parseEmployerFromResponse(value['projectManager']),
                    performers: performers
                }

                projects.push(project);
            })
            
            return projects;
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
        deleteProject(id) {
            axios
                .delete("http://localhost:50598/api/projects/" + id)
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
            
            for (let priorityState in this.filters.priority) {
                if (this.filters.priority[priorityState].enable === true) {
                    params.append("priority", this.filters.priority[priorityState].value);
                }
            }
            
            if (this.filters.searchQuery) {
                params.append("projectName", this.filters.searchQuery);
            }
            
            if (this.filters.startEndDate) {
                params.append("startDateTime", this.filters.startEndDate.start.toISOString());
                params.append("endDateTime", this.filters.startEndDate.end.toISOString());
            }
            
            axios
                .get("http://localhost:50598/api/projects/search", {
                    params: params
                })
                .then(response => {
                    this.projectsList = this.parseProjectsFromResponse(response);
                })
                .catch(err => {
                    // eslint-disable-next-line no-console
                    console.log(err);
                });
        }
    }
}