<template lang="html">
    <sui-container style="margin-top: 2em;margin-bottom: 2em;">
        <sui-grid>
            <sui-grid-row>
                <sui-grid-column centered :width="14">
                    <sui-form @submit.prevent="submit" v-bind:error="errors">
                        <sui-form-field>
                            <label>Task Name</label>
                            <input placeholder="Task Name" v-model="params.taskName.data"/>
                            <sui-message v-bind:error="!params.taskName.error" >
                                <sui-message-header>Task Name Error</sui-message-header>
                                <p>
                                    {{params.taskName.errorMessage}}
                                </p>
                            </sui-message>
                        </sui-form-field>
                        <sui-form-field>
                            <label>Comment</label>
                            <input placeholder="Comment" v-model="params.comment.data"/>
                            <sui-message v-bind:error="!params.comment.error" >
                                <sui-message-header>Comment Error</sui-message-header>
                                <p>
                                    {{params.comment.errorMessage}}
                                </p>
                            </sui-message>
                        </sui-form-field>
                        <sui-form-field>
                            <label>Author</label>
                            <multiselect style="border: 1px solid rgba(34,36,38,.15); border-radius: .28571429rem;"
                                         v-model="params.author.data"
                                         deselect-label="Remove"
                                         track-by="fullName"
                                         label="fullName"
                                         placeholder="Author"
                                         :options="params.author.authorOptions"
                                         :searchable="true"
                                         :allow-empty="true"
                                         :close-on-select="true"
                                         @search-change="onChange"
                                         @open="onOpen"
                            >
                            </multiselect>
                        </sui-form-field>
                        <sui-form-field>
                            <label>Performer</label>
                            <multiselect style="border: 1px solid rgba(34,36,38,.15); border-radius: .28571429rem;"
                                         v-model="params.performer.data"
                                         deselect-label="Remove"
                                         track-by="fullName"
                                         label="fullName"
                                         placeholder="Performer"
                                         :options="params.performer.performerOptions"
                                         :searchable="true"
                                         :allow-empty="true"
                                         :close-on-select="true"
                                         @search-change="onChange"
                                         @open="onOpen"
                            >
                            </multiselect>
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
                        <sui-form-fields grouped>
                            <sui-form-field>
                                <label>Status</label>
                            </sui-form-field>
                            <sui-form-field>
                                <sui-checkbox label="To Do" radio value="1" v-model="params.status.data" />
                            </sui-form-field>
                            <sui-form-field>
                                <sui-checkbox label="In Progress" radio value="2" v-model="params.status.data" />
                            </sui-form-field>
                            <sui-form-field>
                                <sui-checkbox label="Done" radio value="3" v-model="params.status.data" />
                            </sui-form-field>
                        </sui-form-fields>
                        <sui-button>Submit</sui-button>
                    </sui-form>
                </sui-grid-column>
            </sui-grid-row>
        </sui-grid>
    </sui-container>
</template>
<script src="./Task.js"></script>