import axios from 'axios';

const user = {
    namespaced: true,
    state: {
      user: null,
      status: { },
    },
    mutations: {
      setUser(state, user) {
        state.user = user;
        state.status = { loggedIn: true }
      },
      logout(state) {
        state.user = null;
        state.status = {};
      }
    },

    actions: {
      async login({commit}, userDto) {
        let response;
        try {
          response = await axios.post('api/user/login', userDto);
          if (response.status === 200) {
            let {id, username, accountType, products, token} = response.data;
            let user = {
              id: id,
              username: username,
              // Refactor if more than two accountTypes
              accountType: (accountType === 0 ? 'vendor': 'user'),
              products,
              isLoggedIn: true,
              token,
            };
            sessionStorage.user = JSON.stringify(user);
            commit('setUser', user);
            return true;
          } else {
            // Wrong username or password
            return false;
          }
        } catch (error) {
          return false
        }
      },

      async registerUser({commit}, user) {
        let response;
        try {
          response = await axios.post('/api/user/register', user);
          commit('setUser', response.data);
          return response;
        } catch(error) {
          console.error(error);
        }
      },
      logout({commit}) {
        sessionStorage.removeItem('user')
        commit('logout')
      },
    },
    getters: {
    },
}

export default user
