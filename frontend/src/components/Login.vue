<template>
    <div class="container-fluid moore-gradient moore-navy">
        <div class="row">
            <form @submit.prevent='validateLogin'>
                <div class="form-group">
                    <label for="inputUsername">Username</label>
                    <input v-model='username' type="text" class="form-control" id="inputUsername" aria-describedby="usernameHelp" placeholder="Enter username">
                </div>
                <div class="form-group">
                    <label for="inputPassword">Password</label>
                    <input v-model='password' type="password" class="form-control" id="inputPassword" placeholder="Password">
                </div>
                <button type="submit" class="btn btn-primary">Submit</button>
            </form>
        </div>
    </div>
</template>

<script>
import {mapActions} from 'vuex'

export default {
    name: 'Login',

    data() {
        return {
            username: '',
            password: ''
        }
    },

    methods: {
        ...mapActions('user', [
            'login'
        ]),

        async validateLogin() {
            if (this.username && this.password) {
                let userDto = {
                username: this.username,
                password: this.password
                };
                await this.login(userDto);
            }
            
            
        }
    }

}
</script>

<style scoped>

</style>
