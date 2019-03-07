import axios from 'axios';

const vendor = {
  namespaced: true,
  state: {
    allVendors: [],
    currentVendor: {},
  },

  mutations: {
    setCurrentVendor(state, vendor) {
      state.currentVendor = vendor;
    },
    setAllVendors(state, vendors) {
      state.allVendors = vendors;
    },
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
    
  },

  getters: {

  }
}

export default vendor
