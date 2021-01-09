<template>
  <div class="navbar">
    <div class="row">
      <item title="Login" link="/login"></item>
      <item title="2" link="/TODO"></item>
      <item title="3" link="/TODO"></item>
      <item title="4" link="/TODO"></item>
    </div>
  </div>
</template>

<script>
import item from "./NavBar-item"

export default {
  name: "NavBar",
  components: { item }
};
</script>

<style lang="css">
.navbar{
  width: 100%;
  height: 60px;
  background-color: #8e8e8e;
  top: 0;
  /* position: fixed; */
  border-radius:10px;
  display: flex;
}
.row{
  display: flex;
  width: 95%;
  height: 40px;
  margin-top: 10px;
  margin-left: 10px;
}
</style>
