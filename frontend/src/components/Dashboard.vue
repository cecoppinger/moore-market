<template>
    <div class='moore-gradient moore-navy'>
        <p>Current User's name: {{ currentUser.username }} | Account Type: {{ currentUser.accountType }}</p>
        <farm-dashboard v-if="currentUser.accountType === 'vendor'"></farm-dashboard>
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
        ...mapState('user', ['currentUser'])
    }
}
</script>

