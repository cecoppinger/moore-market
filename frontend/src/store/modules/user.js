import axios from 'axios';

const user = {
    namespaced: true,
    state: {
        currentUser: {
            id: 1,
            username: "user",
            accountType: "vendor",
            isLoggedIn: true,
            location: {},
            favoriteMarkets: [1,3,5],
        },
    },

    mutations: {
      setUser(state, user) {
        state.user = user;
      },
    },

    actions: {
      async login({commit}, userDto) {
        let response;
        try {
          response = await axios.post('api/user/login', userDto);
          if (response.status === 200) {
            let {id, username, accountType, token} = response.data;
            let user = {
              id: id,
              username: username,
              // Refactor if more than two accountTypes
              accountType: (accountType === 0 ? 'vendor': 'user'),
              isLoggedIn: true
            };
            sessionStorage.user = JSON.stringify({token:token});
            commit('setUser', user);
            return true;
          } else {
            // Wrong username or password
            return false;
          }
        } catch (error) {
          console.log(error);
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
    },

    getters: {
    },
}

export default user
