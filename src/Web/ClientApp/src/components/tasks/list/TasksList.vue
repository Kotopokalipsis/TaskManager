<template lang="html">
    <sui-container>
        <sui-grid :columns="3" style="margin-top: 1em;">
            <sui-grid-row centered class="center aligned">
                <sui-grid-column class="center aligned" grouped>
                    <label><strong>Status</strong></label>
                    <div class="ui divider"></div>
                    <sui-form class="center aligned">
                        <sui-form-fields equal-width>
                            <sui-form-field><sui-checkbox label="To Do" radio value="1" v-model="filters.status.value" /></sui-form-field>
                            <sui-form-field><sui-checkbox label="In Progress" radio value="2" v-model="filters.status.value" /></sui-form-field>
                            <sui-form-field><sui-checkbox label="Done" radio value="3" v-model="filters.status.value" /></sui-form-field>
                            <sui-form-field><sui-checkbox label="All" radio value="4" v-model="filters.status.value" /></sui-form-field>
                        </sui-form-fields>
                    </sui-form>
                </sui-grid-column>
                <sui-grid-column class="center aligned">
                    <label><strong>Search by Task Name</strong></label>
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
                    <sui-table-header-cell>Task Name</sui-table-header-cell>
                    <sui-table-header-cell>Status</sui-table-header-cell>
                    <sui-table-header-cell>Priority</sui-table-header-cell>
                    <sui-table-header-cell>Author</sui-table-header-cell>
                    <sui-table-header-cell>Performer</sui-table-header-cell>
                    <sui-table-header-cell>Comment</sui-table-header-cell>
                    <sui-table-header-cell>Actions</sui-table-header-cell>
                </sui-table-row>
            </sui-table-header>
    
            <sui-table-body class="teal">
                <sui-table-row class="center aligned" v-for="task in taskList" v-bind:key="task.id">
                    <sui-table-cell><span class="teal">{{ task.taskName }}</span></sui-table-cell>
                    <sui-table-cell><sui-label>{{ task.status }}</sui-label></sui-table-cell>
                    <sui-table-cell><sui-label>{{ task.priority }}</sui-label></sui-table-cell>
                    <sui-table-cell><span class="teal">{{ task.author.fullName }}</span></sui-table-cell>
                    <sui-table-cell><span class="teal">{{ task.performer.fullName }}</span></sui-table-cell>
                    <sui-table-cell><span class="teal">{{ task.comment }}</span></sui-table-cell>
                    <sui-table-cell>
                        <sui-button>
                            <router-link :to="{ name: 'update-task', params: { id: task.id }}" class="black">Update</router-link>
                        </sui-button>
                        <div class="ui divider"></div>
                        <sui-button v-on:click="deleteTask(task.id)">Delete</sui-button>
                    </sui-table-cell>
                </sui-table-row>
            </sui-table-body>
        </sui-table>
    </sui-container>
</template>

<script src="./TasksList.js"></script>