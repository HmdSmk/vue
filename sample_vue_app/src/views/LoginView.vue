<template>
  <div class="login-container">
    <h2>Login</h2>
    <div class="login-container-row">
      <span>User Name:</span>
      <input type="text" class="user-name" v-model="userName" />
    </div>
    <div class="login-container-row">
      <span>Password:</span>
      <input type="password" class="password" v-model="password" />
    </div>
    <div class="login-container-row">
      <router-link to="/signup">
        <button>Sign Up</button>
      </router-link>
      <button @click="login()">Login</button>
    </div>

    <div class="login-container-row"></div>
  </div>
</template>

<script lang="ts" setup>
import { ref } from "vue";
import {authenticate} from '@/mixins/AuthenticationMixin';
import router from '@/router'



const userName = ref("");
const password = ref("");

function login() {
    authenticate(userName.value, password.value)
    .then((data)=>{
        console.log('success', data);
        router.push({name:'home'})
    })
    .catch((error)=>{
          console.log('success', error);
    });
}
</script>

<style scoped lang="scss">
.login-container {
  display: flex;
  flex-direction: column;
  width: 100%;
  justify-items: center;
  align-items: center;
}
.login-container-row {
  display: flex;
  flex-direction: row;
}
</style>
