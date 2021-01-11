<template>
  <div>
    <div class="col-sm-6 offset-sm-3">
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
}
</style>
