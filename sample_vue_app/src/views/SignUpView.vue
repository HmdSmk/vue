<template>
  
  <div class="login-container">
    <h2>Signup</h2>
    <div class="login-container-row">
    <span>User Name:</span>
    <input type="text" class="user-name" v-model="userName">
    </div>
    <div class="login-container-row">
    <span>Password:</span>
    <input type="password" class="password" v-model="userName">
</div>
<div class="login-container-row">
    <span>Confirm Password:</span>
    <input type="password" class="password" v-model="userName">
</div>
<div class="login-container-row">
    <router-link to="/login">
        <button>Login</button>
    </router-link>
    <button @click="signUp()">Sign Up</button>
    </div>

    <div class="login-container-row"></div>
  </div>

  
</template>

<script lang="ts" setup>
import { ref } from "vue";
const userName = ref("");

function signUp() { 
    console.log('signUp clicked.')
}
</script>

<style scoped lang="scss">
.login-container
{
    display: flex;
    flex-direction:column;
    width:100%;
    justify-items: center;
    align-items: center;
}
.login-container-row{
    display: flex;
    flex-direction:row;
}
</style>
