import axios from 'axios';

const api = {
    namespaced: true,
    state: {
    },

    mutations: {
      
    },

    actions: {
      createAxiosAuth() {
        if(sessionStorage.user) {
            let {token} = JSON.parse(sessionStorage.user)
            let axiosAuth = axios.create({
                headers: {
                    "X-Requested-With": "XMLHttpRequest",
                    "Content-Type": "application/json",
                    "Accept": "*/*",
                    "Authorization": `Bearer ${token}`
                }
            });
            return axiosAuth;
        } else {
            return false;
        }

      },
    },

    getters: {
    },
}

export default api
