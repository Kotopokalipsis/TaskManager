<template lang="html">
    <div>
        <sui-container style="margin-top: 2em;margin-bottom: 2em;">
            <sui-grid>
                <sui-grid-row>
                    <sui-grid-column centered :width="14">
                        <sui-form @submit.prevent="submit" v-bind:error="errors">
                            <sui-form-field>
                                <label>First Name</label>
                                <input placeholder="First Name" type="text" v-model="params.firstName.data"/>
                                <sui-message v-bind:error="!params.firstName.error" >
                                    <sui-message-header>First Name Error</sui-message-header>
                                    <p>
                                        {{this.params.firstName.errorMessage}}
                                    </p>
                                </sui-message>
                            </sui-form-field>
                            <sui-form-field>
                                <label>Last Name</label>
                                <input placeholder="Last Name" v-model="params.lastName.data"/>
                                <sui-message v-bind:error="!params.lastName.error" >
                                    <sui-message-header>Last Name Error</sui-message-header>
                                    <p>
                                        {{params.lastName.errorMessage}}
                                    </p>
                                </sui-message>
                            </sui-form-field>
                            <sui-form-field>
                                <label>Patronymic Name</label>
                                <input placeholder="Patronymic Name" v-model="params.patronymicName.data"/>
                                <sui-message v-bind:error="!params.patronymicName.error" >
                                    <sui-message-header>Patronymic Name Error</sui-message-header>
                                    <p>
                                        {{params.patronymicName.errorMessage}}
                                    </p>
                                </sui-message>
                            </sui-form-field>
                            <sui-form-field>
                                <label>Email</label>
                                <input placeholder="Email" type="email" v-model="params.email.data"/>
                                <sui-message v-bind:error="!params.email.error" >
                                    <sui-message-header>Email Error</sui-message-header>
                                    <p>
                                        {{params.email.errorMessage}}
                                    </p>
                                </sui-message>
                            </sui-form-field>
                            <sui-button>Submit</sui-button>
                        </sui-form>
                    </sui-grid-column>
                </sui-grid-row>
            </sui-grid>
        </sui-container>
    </div>
</template>

<script src="./Employer.js"></script>