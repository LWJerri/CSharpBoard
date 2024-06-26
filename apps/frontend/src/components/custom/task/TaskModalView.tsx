import { Badge } from "@/components/ui/badge";
import { Dialog, DialogContent, DialogFooter, DialogHeader, DialogTitle } from "@/components/ui/dialog";
import { Label } from "@/components/ui/label";
import { Task } from "@/types/Task";
import { IconCalendarDue, IconTimelineEventPlus } from "@tabler/icons-react";
import { format } from "date-fns";
import Markdown from "react-markdown";
import rehypeHighlight from "rehype-highlight";
import remarkGfm from "remark-gfm";
import { Priority } from "../../../enums/priority";

const TaskModalView = (props: { open: boolean; close: () => void; task: Task }) => {
  const { open, close, task } = props;

  return (
    <Dialog open={open} onOpenChange={close}>
      <DialogContent className="max-h-full">
        <DialogHeader>
          <DialogTitle>{task.title}</DialogTitle>
        </DialogHeader>

        <Markdown
          rehypePlugins={[rehypeHighlight]}
          remarkPlugins={[remarkGfm]}
          className="small max-h-full min-h-96 overflow-y-auto break-all font-normal"
        >
          {task.description}
        </Markdown>

        <DialogFooter>
          <div className="grid w-full items-center gap-4">
            <div className="flex w-full justify-between">
              <div className="flex items-center sm:space-x-1.5">
                <IconCalendarDue stroke={1.5} />
                <Label htmlFor="name">{format(task.dueAt, "dd.MM.yyyy")}</Label>
              </div>

              <div className="flex items-center sm:space-x-1.5">
                <IconTimelineEventPlus stroke={1.5} />
                <Label htmlFor="name">{format(task.createdAt, "dd.MM.yyyy")}</Label>
              </div>

              <Badge variant={task.priority === 3 || task.priority === 2 ? "destructive" : "secondary"}>
                {Priority[task.priority]}
              </Badge>
            </div>
          </div>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
};

export default TaskModalView;
