// import { useStorage } from '@vueuse/core';
import { useAuthStore } from '@/store/authStore';
import axios from 'axios';

export function authenticatedUserName() {
    // return useStorage('login-user-name', '') // returns Ref<string>

    const authStore = useAuthStore();
    return authStore.loggedOnUserName;
}

// export function authenticate(userName: string, password: string) :Observable<{success:boolean}>{
//     const success = userName == '1' && password == '1';
//     if (success) {
//         const authStore = useAuthStore();
//         authStore.setLoggedOnUserName(userName);
//     }
//     return of({ success: success });
// }

const API_BASE_URL = 'http://localhost:5268'; // Replace with your actual API URL

export const authenticate = (username: string, password: string) => {
    // Concatenate and encode username and password in base64
    const credentials = btoa(`${username}:${password}`);

    const config = {
        headers: {
            Authorization: `Basic ${credentials}`,
        },
    };

    return new Promise((resolve, reject) => {
        axios.post(`${API_BASE_URL}/Auth/GetUserToken`, { username, password }, config)
            .then(response => {
                localStorage.setItem('TOKEN_KEY', response.data);


                // Update session state
                const authStore = useAuthStore();
                authStore.setLoggedOnUserName(username);
                
                resolve(response.data);
            })
            .catch(error => reject(error));
    });
};
