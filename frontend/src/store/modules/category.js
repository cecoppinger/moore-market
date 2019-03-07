import axios from 'axios'

const category = {
    namespaced: true,

    state: {
        allCategories: []
    },

    mutations: {

        setAllCategories (state, categories) {
            state.allCategories = categories;
        },

        pushNewCategory (state, category) {
            state.allCategories.push(category);
        },

        editCategoryByIndex (state, payload) {
            state.allCategories.splice(payload.idx, 1, payload.category);
        },

        removeCategoryByIndex (state, idx) {
            state.allCategories.splice(idx, 1);
        },

    },

    actions: {

        async getAllCategoriesFromApi({commit}) {
            let response;
            try {
                response = await axios.get('/api/categories');
                commit('setAllCategories', response.data);
                return response;
            } catch (error) {
                console.error(error);
            }
            
        },

        async addNewCategory({commit, dispatch}, category) {
            let response
            try {
                let axiosAuth = await dispatch('api/createAxiosAuth', '' ,{root:true});
                if (axiosAuth) {
                   response = await axiosAuth.post('/api/categories/add', category);
                    commit('pushNewCategory', response.data);
                    return response ;
                } else {
                    //No token
                    return false;
                }
            } catch (error) {
                console.error(error);
            }
            
        },

        async editCategoryById({ commit, getters, dispatch}, category) {
            let response
            try {
                let axiosAuth = await dispatch('api/createAxiosAuth', '' ,{root:true});
                if (axiosAuth) {
                    response = await axiosAuth.put('/api/categories/edit/' + category.id, category);
                    let idx = getters.getCategoryIndex(response.data.id);
                    let payload = {'idx':idx, 'category': response.data};
                    commit('editCategoryByIndex', payload);
                    return response;
                } else {
                    //No token
                    return false;
                }
            } catch (error) {
                console.error(error);
            }
            
        },

        async removeCategoryById({ commit, getters, dispatch}, id) {
            let response
            try {
                let axiosAuth = await dispatch('api/createAxiosAuth', '' ,{root:true});
                if (axiosAuth) {
                    response = await axiosAuth.delete('/api/categories/remove/' + id);
                    let idx = getters.getCategoryIndex(id);
                    commit('removeCategoryByIndex', idx);
                    return response;
                } else {
                    //No token
                    return false;
                }
            } catch (error) {
                console.error(error);
            }
            
        },


    },

    getters: {

        getCategoryIndex: (state) => (id) => {
            return state.allCategories.findIndex(c => c.id === id);
        },

    }
}

export default category
