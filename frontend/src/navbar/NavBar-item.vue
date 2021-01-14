<template>
  <router-link :to="link" tag="div" class="navbar_item">{{ title }}</router-link>
</template>

<script>
export default {
  name: "NavBar-item",
  props: ["title", "link"]
};
</script>

<style lang="css">
.navbar_item {
  display: block;
  text-align: center;
  padding: 0.5rem 2rem;
  cursor: pointer;
  text-transform: uppercase;
  font-size: 1.2rem;
  line-height: 1.5rem;
}

.navbar_item:hover {
  background-color: var(--primary-color);
  color: black;
}
</style>
