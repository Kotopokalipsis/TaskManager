import axios from "axios";

function getEmployeesList() {
    return axios
        .get("http://localhost:50598/api/employees")
        .then(response => {
            return parseSearchData(response.data);
        })
        .catch(err => {
            // eslint-disable-next-line no-console
            console.log(err);
        });
}

async function getEmployees(query) {
    return axios
        .get("http://localhost:50598/api/employees/search", {
            params: {
                query: query,
            }
        })
        .then(response => {
            let data = parseSearchData(response.data);

            if (!Array.isArray(data)) {
                data = [].push(data);
            }

            return data;
        })
        .catch(err => {
            // eslint-disable-next-line no-console
            console.log(err);
        });
}

function parseSearchData(response) {
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
}

export { getEmployeesList, parseSearchData, getEmployees}