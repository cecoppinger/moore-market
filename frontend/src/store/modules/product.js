import axios from 'axios';

const product = {
    namespaced: true,
    
    state: { 
        allProducts: [],
        cart: [],
        searchResults: [],
    },

    mutations: {
        setNewProduct(state, product) {
            state.allProducts.push(product);
        },
        updateAllProducts(state, products) {
            state.allProducts = products;
        },
        removeProductByIndex(state, idx) {
            state.allProducts.splice(idx, 1);
        },
        addEditedProduct(state, payload) {
            let {idx, product} = payload
            state.allProducts.splice(idx, 1, product);
        },
        addProductToCart(state, product) {
            state.cart.push(product);
        },
        updateSearchResults(state, products) {
          state.searchResults = products;
        }
    },

    actions: { 
        async getAllProductsFromApi({commit}) {
            let response;
            try{
                response = await axios.get('/api/products');
                commit('updateAllProducts', response.data);
                return response;
            } catch (error) {
                console.log(error);
            }
        },
        
        async addNewProduct({commit, dispatch}, product) {
            let response;
            try{
                let axiosAuth = await dispatch('api/createAxiosAuth', '' ,{root:true});
                if (axiosAuth) {
                    response = await axiosAuth.post('/api/products/add', product);
                    commit('setNewProduct', response.data);
                    return response;
                } else {
                    //No token
                    return false;
                }
            } catch (error) {
                console.log(error);
            }
        },

        async editProductById({ commit, getters, dispatch}, product) {
            let response
            try{
                let axiosAuth = await dispatch('api/createAxiosAuth', '' ,{root:true});
                if (axiosAuth) {
                    response = await axiosAuth.put('/api/products/edit/'+ product.id, product);
                    let idx = getters.getProductIndex(response.data.id);
                    let payload = {'idx':idx, 'product':response.data};
                    commit('addEditedProduct', payload);
                    return response;
                } else {
                    //No token
                    return false;
                }
            } catch(error) {
                console.log(error);
            }
        },

        async removeProductById({commit, getters, dispatch}, id) {
            let response
            try{
                let axiosAuth = await dispatch('api/createAxiosAuth', '' ,{root:true});
                if (axiosAuth) {
                    response = await axiosAuth.delete('/api/products/remove/'+ id);
                    let idx = getters.getProductIndex(id);
                    commit('removeProductByIndex', idx);
                    return response;
                } else {
                    //No token
                    return false;
                }
            } catch(error) {
                console.log(error);
            }
            
        },

        addProductToCart({commit}, product) {
            return commit('addProductToCart', product);
        },

        async searchProducts({commit}, searchTerm) {
            let response;
            try {
                response = await axios.get('/api/products/search?searchTerm='+searchTerm);
                return commit('updateSearchResults', response.data);
            } catch(error) {
                console.error(error);
            }
        },
        
    },

    getters: {

        getProductById: (state) => (id) => {
            return state.allProducts.find(p => p.id === id)
        },

        getProductIndex: (state) => (id) => {
            return state.allProducts.findIndex(p => p.id === id)
        }
    }

}

export default product
