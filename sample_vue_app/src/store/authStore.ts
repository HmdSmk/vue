import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useAuthStore = defineStore('authStore', () => {
  const loggedOnUserName = ref('');

  function setLoggedOnUserName(userName: string) {
    loggedOnUserName.value = userName;
  }

  return {
    loggedOnUserName,
    setLoggedOnUserName
  };
});
