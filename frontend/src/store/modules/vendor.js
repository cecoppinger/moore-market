import axios from 'axios';

const vendor = {
  namespaced: true,
  state: {
    allVendors: [],
    currentVendor: {},
    allMessages: [],
    vendorMessages: [],
  },

  mutations: {
    setCurrentVendor(state, vendor) {
      state.currentVendor = vendor;
    },
    setAllVendors(state, vendors) {
      state.allVendors = vendors;
    },
    setAllMessages(state, messages) {
      state.allMessages = messages
    },
    setNewMessage(state, message) {
      state.vendorMessages.push(message)
    },
    setVendorMessages(state, messages) {
      state.vendorMessages = messages
    },
    addEditedMessage(state, payload) {
      state.vendorMessages.splice(payload.idx, 1, payload.message)
    },
    removeMessageByIndex(state, idx) {
      state.vendorMessages.splice(idx, 1)
    }
  },
  
  actions: {
    async getAllVendors({commit}) {
      let response
      try {
        response = await axios.get('/api/vendors');
        commit('setAllVendors', response.data);
        return response;
      } catch (error) {
        console.log(error);
      }
    },
    async getVendor({commit}, id) {
      let response
      try {
        response = await axios.get('/api/vendors/'+ id);
        commit('setCurrentVendor', response.data);
      } catch(error) {
        console.log(error);
      }
      
    },
    getAllMessagesFromApi({ commit }) {
      axios.get('/api/messages')
        .then(response => commit('setAllMessages', response.data))
    },
    // requires token
    addNewMessage({ commit }, message) {
      axios.post('/api/messages/add', message)
        .then((res) => commit('setNewMessage', res.data))
    },
    // requires token
    getVendorMessages({ commit }, id) {
      axios.get('api/vendors/'+id+'/messages')
        .then(response => commit('setVendorMessages', response.data))
    },
    // requires token
    async editMessageById({ commit, getters }, message) {
      let response
      try{
          response = await axios.put('/api/messages/edit/'+ message.id, message)
          let idx = getters.getMessageIndex(response.data.id)
          let payload = {'idx':idx, 'message':response.data}
          commit('addEditedMessage', payload)
          return response
      } catch(error) {
          console.log(error)
      }
    },
    // requires token
    async removeMessageById({ commit, getters }, id) {
      let response
      try {
        response = await axios.delete('/api/messages/remove/'+id)
        let idx = getters.getMessageIndex(id)
        commit('removeMessageByIndex', idx)
        return response
      }
      catch(error) {
        console.log(error)
      }
    }
    
  },

  getters: {
    getMessageIndex: (state) => (id) => {
      return state.vendorMessages.findIndex(m => m.id === id)
    },

    getMessageById: (state) => (id) => {
      return state.vendorMessages.find(m => m.id === id)
    }
  }
}

export default vendor
