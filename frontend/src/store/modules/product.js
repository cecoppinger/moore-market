import axios from 'axios';

const product = {
    namespaced: true,
    state: { 
        newProduct: null,
        allProducts: [],
    },

    mutations: {
        setNewProduct (state, product) {
            state.newProduct = product
            state.allProducts.push(product)
        },
        updateProducts(state, products) {
            state.allProducts = products
        },
        removeProductByIndex(state, idx) {
            state.allProducts.splice(idx, 1)
        },
        addEditedProduct(state, idx, product) {
            state.allProducts.splice(idx, 1, product)
        }
    },
    actions: { 
        addNewProduct({ commit }, product) {
            return axios.post('/api/products/add', product).then(() => commit('setNewProduct', product))
        },
        getAllProductsFromApi({ commit }) {
            axios.get('/api/products').then(response => commit('updateProducts', response.data))
        },
        removeProductById({ commit, getters }, id) {
            let idx = getters.getProductIndex(id)
            axios.delete('/api/products/remove/'+id).then(() => commit('removeProductByIndex', idx))   
        },
        editProductById({ commit, getters }, product) {
            let idx = getters.getProductIndex(product.id)
            axios.put('/api/products/edit/'+product.id, product).then(() => commit('addEditedProduct', idx, product))
        }
    },
    getters: {
      //this syntax lets us call getters with a parameter
        getProductById: (state) => (id) => {
            return state.allProducts.find(p => p.id === id)
        },
        getProductIndex: (state) => (id) => {
            return state.allProducts.findIndex(p => p.id === id)
        }
    }
}

export default product