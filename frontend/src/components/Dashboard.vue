<template>
    <div class='moore-gradient moore-navy'>
        <p v-if="user">Current User's name: {{ user.username }} | Account Type: {{ user.accountType }}</p>
        <farm-dashboard v-if="user && user.accountType === 'vendor'"></farm-dashboard>
        <user-dashboard v-else></user-dashboard>
    </div>
</template>

<script>
import UserDashboard from '../components/UserDashboard.vue'
import FarmDashboard from '../components/FarmDashboard.vue'
import { mapState } from 'vuex'

export default {
    components: {
        'farm-dashboard': FarmDashboard,
        'user-dashboard': UserDashboard,        
    },

    computed: {
        ...mapState('user', ['user'])
    }
}
</script>

