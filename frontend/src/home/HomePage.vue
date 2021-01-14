<template>
  <div>
    <div class="main_column">
      <img id="main" src="../../assets/concert.gif" />
      <concertSelection></concertSelection>
    </div>
  </div>
</template>

<script>
import concertSelection from "./ConcertSelector";

export default {
  components: { concertSelection }
};
</script>

<style lang="css" scoped>
img#main {
  width: 100%;
  padding-bottom: 2rem;
}
</style>
