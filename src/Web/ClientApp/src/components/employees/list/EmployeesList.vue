<template lang="html">
    <sui-container>
        <sui-table celled style="margin-top: 1.5em;" color="teal">
            <sui-table-header class="teal">
                <sui-table-row class="center aligned">
                    <sui-table-header-cell>First Name</sui-table-header-cell>
                    <sui-table-header-cell>Last Name</sui-table-header-cell>
                    <sui-table-header-cell>Patronymic Name</sui-table-header-cell>
                    <sui-table-header-cell>Email</sui-table-header-cell>
                    <sui-table-header-cell>Actions</sui-table-header-cell>
                </sui-table-row>
            </sui-table-header>
    
            <sui-table-body class="teal">
                <sui-table-row class="center aligned" v-for="employer in employeesList" v-bind:key="employer.id">
                    <sui-table-cell><span class="teal">{{ employer.firstName }}</span></sui-table-cell>
                    <sui-table-cell><span class="teal">{{ employer.lastName }}</span></sui-table-cell>
                    <sui-table-cell><span class="teal">{{ employer.patronymicName }}</span></sui-table-cell>
                    <sui-table-cell><span class="teal">{{ employer.email }}</span></sui-table-cell>
                    <sui-table-cell>
                        <sui-button>
                            <router-link :to="{ name: 'update-employer', params: { id: employer.id }}" class="black">Update</router-link>
                        </sui-button>
                        <div class="ui divider"></div>
                        <sui-button v-on:click="deleteEmployer(employer.id)">Delete</sui-button>
                    </sui-table-cell>
                </sui-table-row>
            </sui-table-body>
        </sui-table>
    </sui-container>
</template>

<script src="./EmployeesList.js"></script>