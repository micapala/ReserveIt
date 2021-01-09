<template>
  <div class="navbar-item">
    <router-link :to="link" tag="h4">{{ title }}</router-link>
  </div>
</template>

<script>
export default {
  name: "NavBar-item",
  props: ['title', 'link']
};
</script>

<style lang="css">
.navbar-item{
  width: 80px;
  height: 34px;
  margin-left: 20px;
  display: flex;
}
</style>
