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
  display: block;
  color: var(--font-color);
  text-align: center;
  padding: .5rem 2rem;
}

.navbar-item:hover{
  background-color: var(--primary-color);
  color: black;
}
</style>
