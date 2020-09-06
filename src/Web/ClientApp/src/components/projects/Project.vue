<template lang="html">
    <div>
        <sui-container style="margin-top: 2em;margin-bottom: 2em;">
            <sui-grid>
                <sui-grid-row>
                    <sui-grid-column centered :width="14">
                        <sui-form @submit.prevent="submit" v-bind:error="errors">
                            <sui-form-field>
                                <label>Project Name</label>
                                <input placeholder="Project Name" v-model="params.projectName.data"/>
                                <sui-message v-bind:error="!params.projectName.error" >
                                    <sui-message-header>Project Name Error</sui-message-header>
                                    <p>
                                        {{params.projectName.errorMessage}}
                                    </p>
                                </sui-message>
                            </sui-form-field>
                            <sui-form-field>
                                <label>Performer Company</label>
                                <input placeholder="Performer Company" v-model="params.performerCompany.data"/>
                                <sui-message v-bind:error="!params.performerCompany.error" >
                                    <sui-message-header>Performer Company Error</sui-message-header>
                                    <p>
                                        {{params.performerCompany.errorMessage}}
                                    </p>
                                </sui-message>
                            </sui-form-field>
                            <sui-form-field>
                                <label>Сustomer Сompany</label>
                                <input placeholder="Сustomer Сompany" v-model="params.customerCompany.data"/>
                                <sui-message v-bind:error="!params.customerCompany.error" >
                                    <sui-message-header>Сustomer Сompany Error</sui-message-header>
                                    <p>
                                        {{params.customerCompany.errorMessage}}
                                    </p>
                                </sui-message>
                            </sui-form-field>
                            <sui-form-field>
                                <label>Project Manager</label>
                                <multiselect style="border: 1px solid rgba(34,36,38,.15); border-radius: .28571429rem;"
                                        v-model="params.projectManager.data" 
                                        deselect-label="Remove" 
                                        track-by="fullName" 
                                        label="fullName" 
                                        placeholder="Project Manager"
                                        :options="params.projectManager.projectManagerOptions" 
                                        :searchable="true" 
                                        :allow-empty="true"
                                        :close-on-select="true"
                                        @search-change="onChange"
                                        @open="onOpen"
                                >
                                </multiselect>
                            </sui-form-field>
                            <sui-form-field>
                                <label>Performers</label>
                                <multiselect style="border: 1px solid rgba(34,36,38,.15); border-radius: .28571429rem;"
                                        v-model="params.performers.data"
                                        :multiple="true"
                                        deselect-label="Remove"
                                        track-by="fullName"
                                        label="fullName"
                                        placeholder="Performers"
                                        :options="params.performers.performerOptions"
                                        :searchable="true"
                                        :allow-empty="true" 
                                        :close-on-select="false"
                                        @search-change="onChange"
                                        @open="onOpen"
                                >
                                </multiselect>
                            </sui-form-field>
                            <sui-form-field>
                                <label>Start-end Dates</label>
                                <date-picker mode='range' v-model='params.range.data'></date-picker>
                            </sui-form-field>
                            <sui-form-fields grouped> 
                                <sui-form-field>
                                    <label>Priority</label>
                                </sui-form-field>
                                <sui-form-field>
                                    <sui-checkbox label="High" radio value="1" v-model="params.priority.data" />
                                </sui-form-field>
                                <sui-form-field>
                                    <sui-checkbox label="Normal" radio value="2" v-model="params.priority.data" />
                                </sui-form-field>
                                <sui-form-field>
                                    <sui-checkbox label="Low" radio value="3" v-model="params.priority.data" />
                                </sui-form-field>
                            </sui-form-fields>
                            <sui-button>Submit</sui-button>
                        </sui-form>
                    </sui-grid-column>
                </sui-grid-row>
            </sui-grid>
        </sui-container>
    </div>
</template>

<script src="./Project.js"></script>