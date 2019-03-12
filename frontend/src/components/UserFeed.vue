<template>
    <div class="card border-success mx-2 mb-3">
      <h4 class="card-header">Feed Messages</h4>
      <div class="card-body">
          <ul class="list-group list-group-flush">
              <li v-for="message in allMessages" :key="message.id" class="list-group-item">
                  {{ message.body }} | posted by: <span class="badge badge-pill badge-dark">{{ message.user.username }}</span>
              </li>
              
          </ul>
      </div>
    </div>      
</template>

<script>
export default {
    name: "UserFeed",
    props: ['allMessages']
}
</script>

<style>
</style>