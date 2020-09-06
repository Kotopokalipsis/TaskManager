<template lang="html">
  <div id="app" class="App">
    <nav class="App__nav">
      <sui-container fluid>
        <sui-menu :widths="4" class="ui menu radius">
          <sui-menu-item class="ui small header" style="padding: 0;"><router-link to="/" class="item">Home</router-link></sui-menu-item>
          <sui-menu-item class="ui small header">
            <sui-dropdown text="Projects">
              <sui-dropdown-menu>
                <router-link to="/projects" class="item">Projects List</router-link>
                <router-link to="/projects/create" class="item">Create Project</router-link>
              </sui-dropdown-menu>
            </sui-dropdown>
          </sui-menu-item>
          <sui-menu-item class="ui small header">
            <sui-dropdown text="Employees">
              <sui-dropdown-menu>
                <router-link to="/employees" class="item">Employees List</router-link>
                <router-link to="/employees/create" class="item">Create Employer</router-link>
              </sui-dropdown-menu>
            </sui-dropdown>
          </sui-menu-item>
          <sui-menu-item class="ui small header">
            <sui-dropdown text="Tasks">
              <sui-dropdown-menu>
                <router-link to="/tasks" class="item">Tasks List</router-link>
                <router-link to="/tasks/create" class="item">Create Task</router-link>
              </sui-dropdown-menu>
            </sui-dropdown>
          </sui-menu-item>
        </sui-menu>
      </sui-container>
    </nav>
    <router-view></router-view>
  </div>
</template>

<script>
  export default {
    name: 'App',
  }
</script>
