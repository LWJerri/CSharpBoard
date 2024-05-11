import { ModeToggle } from "@/components/ui/mode-toggle";
import { IconPlus } from "@tabler/icons-react";
import { useState } from "react";
import { Button } from "../../ui/button";
import CreateList from "../list/CreateList";

const Navbar = () => {
  const [openNewList, setOpenNewList] = useState(false);

  return (
    <div>
      <div className="flex w-full flex-col justify-between space-y-1 p-2.5 sm:flex-row sm:items-center sm:space-y-0">
        <div className="flex grow items-center justify-between sm:mr-4">
          <h1 className="h3 text-center font-bold md:text-left">CSharpBoard</h1>

          <ModeToggle />
        </div>

        <Button onClick={() => setOpenNewList(!openNewList)}>
          <IconPlus stroke={1.5} className="mr-2" />
          Create new list
        </Button>
      </div>

      <CreateList open={openNewList} close={() => setOpenNewList(!openNewList)} />
    </div>
  );
};

export default Navbar;
