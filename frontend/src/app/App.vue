<template>
  <div class="jumbotron">
  <NavBar></NavBar>
    <div class="container">
      <div class="row">
        <div class="col-sm-6 offset-sm-3">
          <div v-if="alert.message" :class="`alert ${alert.type}`">
            {{ alert.message }}
          </div>
          <router-view></router-view>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import NavBar from "../navbar/NavBar"

export default {
  name: "app",
  computed: {
    alert() {
      return this.$store.state.alert;
    }
  },
  watch: {
    $route() {
      this.$store.dispatch("alert/clear");
    }
  },
  components: { NavBar }
};
</script>
