<template lang="html">
    <sui-container>
        <sui-grid :columns="4" style="margin-top: 1em;">
            <sui-grid-row centered class="center aligned">
                <sui-grid-column class="center aligned" grouped>
                    <label><strong>Priority</strong></label>
                    <div class="ui divider"></div>
                    <sui-form class="center aligned">
                        <sui-form-fields equal-width>
                            <sui-form-field><sui-checkbox label="High" v-model="filters.priority.high.enable" /></sui-form-field>
                            <sui-form-field><sui-checkbox label="Normal" v-model="filters.priority.normal.enable" /></sui-form-field>
                            <sui-form-field><sui-checkbox label="Low" v-model="filters.priority.low.enable" /></sui-form-field>
                        </sui-form-fields>
                    </sui-form>
                </sui-grid-column>
                <sui-grid-column class="center aligned">
                    <label><strong>Start-End Range</strong></label>
                    <div class="ui divider"></div>
                    <date-picker mode='range' v-model='filters.startEndDate'></date-picker>
                </sui-grid-column>
                <sui-grid-column class="center aligned">
                    <label><strong>Search by Project Name</strong></label>
                    <div class="ui divider"></div>
                    <sui-input placeholder="Search" class="fluid" v-model="filters.searchQuery"/>
                </sui-grid-column>
                <sui-grid-column class="center aligned" >
                    <sui-icon name="close" color="teal"/>
                    <div class="ui divider"></div>
                    <sui-button class="fluid" v-on:click="search">Search</sui-button>
                </sui-grid-column>
            </sui-grid-row>
        </sui-grid>
        <sui-table celled style="margin-top: 1.5em;" color="teal">
            <sui-table-header class="teal">
                <sui-table-row class="center aligned">
                    <sui-table-header-cell>Project Name</sui-table-header-cell>
                    <sui-table-header-cell>Customer Company</sui-table-header-cell>
                    <sui-table-header-cell>Performer Company</sui-table-header-cell>
                    <sui-table-header-cell>Project Manager</sui-table-header-cell>
                    <sui-table-header-cell>Performers</sui-table-header-cell>
                    <sui-table-header-cell>Priority</sui-table-header-cell>
                    <sui-table-header-cell>Start Date</sui-table-header-cell>
                    <sui-table-header-cell>End Date</sui-table-header-cell>
                    <sui-table-header-cell>Actions</sui-table-header-cell>
                </sui-table-row>
            </sui-table-header>
    
            <sui-table-body class="teal">
                <sui-table-row class="center aligned" v-for="project in projectsList" v-bind:key="project.id">
                    <sui-table-cell><span class="teal">{{ project.projectName }}</span></sui-table-cell>
                    <sui-table-cell><span class="teal">{{ project.customerCompany }}</span></sui-table-cell>
                    <sui-table-cell><span class="teal">{{ project.performerCompany }}</span></sui-table-cell>
                    <sui-table-cell><span class="teal">{{ project.projectManager.fullName }}</span></sui-table-cell>
                    <sui-table-cell>
                        <sui-list v-for="performer in project.performers" v-bind:key="performer.id">
                            <sui-list-item><small>{{ performer.fullName }}</small></sui-list-item>
                        </sui-list>
                    </sui-table-cell>
                    <sui-table-cell>
                        <sui-label>{{ project.priority }}</sui-label>
                    </sui-table-cell>
                    <sui-table-cell>{{ project.startDateTime }}</sui-table-cell>
                    <sui-table-cell>{{ project.endDateTime }}</sui-table-cell>
                    <sui-table-cell>
                        <sui-button>
                            <router-link :to="{ name: 'update-project', params: { id: project.id }}" class="black">Update</router-link>
                        </sui-button>
                        <div class="ui divider"></div>
                        <sui-button v-on:click="deleteProject(project.id)">Delete</sui-button>
                    </sui-table-cell>
                </sui-table-row>
            </sui-table-body>
        </sui-table>
    </sui-container>
</template>

<script src="./ProjectsList.js"></script>