<template>
    <div class="container-fluid moore-gradient moore-navy">
        <div v-if="!status.loggedIn && attempted">Username or password was incorrect</div>
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
import { mapActions, mapState } from 'vuex'

export default {
    name: 'Login',

    data() {
        return {
            username: '',
            password: '',
            attempted: false,
        }
    },
    computed: {
      ...mapState('user', ['status'])
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
                let success = await this.login(userDto);
                if(success) {
                  this.$router.push('/dashboard')
                } else {
                  this.attempted = true
                }
            }
            
            
        }
    }

}
</script>

<style scoped>

</style>
