<template>
  <div>
    <!--div class="col-sm-6 offset-sm-3"-->
    <div class="homepage">
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
.homepage {
  max-width: 960px;
  margin: 0 auto;
  padding: 2rem 2.5rem;
}
img#main {
  width: 100%;
}
</style>
